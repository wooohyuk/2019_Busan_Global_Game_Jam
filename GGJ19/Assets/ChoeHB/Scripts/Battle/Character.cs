using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using System;

using Random = UnityEngine.Random;
using Sirenix.OdinInspector;

public class Status
{
    public string name;
    public string id;
    public int hp;
    public int damage;
    public float attackSpeed;
    public bool isUsed;
    public Character prefab;
    public Sprite sprite;
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public abstract class Character : MonoBehaviour
{

    public event Action<Character> OnAttack;
    public event Action<Character> OnKill;
    public event Action<Character> OnDead;

    [ShowInInspector, ReadOnly] public int maxHp             { get; private set; }
    [ShowInInspector, ReadOnly] public int damage               { get; private set; }
    [ShowInInspector, ReadOnly] protected float attackSpeed     { get; private set; }
    [ShowInInspector, ReadOnly] public int hp                   { get; protected set; }
    [ShowInInspector, ReadOnly] public float coolTime           => 1 / attackSpeed;


    [SerializeField] protected List<string> enemyTags;
    [SerializeField] bool targettingSkill;
    [SerializeField] Skill skill;

    [SerializeField] bool useHitEffect;
    [ShowIf(nameof(useHitEffect))]
    [SerializeField] string hitEffect;

    [SerializeField] protected Sight sight;
    [SerializeField] protected CharacterAnimator animator;

    public Sight range => sight;

    protected List<Character> targets;
    protected bool isDead;
    protected bool waitAttack;

    private TextPool damageText;
    private TextPool healText;

    protected virtual void Reset()
    {
        enemyTags = new List<string>();
        sight = GetComponentInChildren<Sight>();
        if (sight == null)
            sight = new GameObject("Sight").AddComponent<Sight>();
        sight.transform.SetParent(transform);
        GetComponent<CircleCollider2D>().isTrigger = true;
        var rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    public void Heal(Attack attack)
    {
        hp += attack.damage;
        attack.OnHit?.Invoke(this);
        healText.Float(attack.damage.ToString(), transform.position + Vector3.up * 0.5f);
    }

    public virtual void Slow(Attack attack, object sender, float value, float during) { }

    protected virtual void Awake()
    {
        targets = new List<Character>();
        foreach (var tag in enemyTags)
            sight.AddTargetTag(tag);

        sight.OnDetectIn += target =>
        {
            Character t = target.GetComponent<Character>();
            t.OnDead += RemoveTarget;
            targets.Add(t);
        };

        sight.OnDetectOut += target =>
        {
            Character t = target.GetComponent<Character>();
            RemoveTarget(t);
        };

        damageText = CacheObjectManager.GetAsStatic<TextPool>("DamageText");
        healText = CacheObjectManager.GetAsStatic<TextPool>("HealText");

    }

    private void RemoveTarget(Character target)
    {
        if (!targets.Contains(target))
            return;
        target.OnDead -= RemoveTarget;
        targets.Remove(target);
    }

    protected virtual void OnEnable()
    {
        GetComponents<Collider2D>().ForEach(c => c.enabled = true);
        isDead = false;
        hp = maxHp;
        StartCoroutine(Attacking());
        targets.Clear();
    }

    protected void Dead()
    {
        GetComponents<Collider2D>().ForEach(c => c.enabled = false);
        StopAllCoroutines();
        isDead = true;
        animator.Dead();
        Invoke(nameof(Disable), 1);
        OnDead?.Invoke(this);
    }

    private void Disable() => gameObject.SetActive(false);


    protected virtual void Initialize(int maxHp, int damage, float attackSpeed)
    {
        this.maxHp = maxHp;
        this.damage = damage;
        this.attackSpeed = attackSpeed;
        
        hp = maxHp;
    }

    public void Hited(Attack attack)
    {
        hp -= attack.damage;

        damageText.Float(attack.damage.ToString(), transform.position + Vector3.up * 0.5f);

        if (useHitEffect)
            EffectAnimator.Play(hitEffect, transform.position);

        if (hp < 0)
            hp = 0;

        attack.OnHit?.Invoke(this);

        if(hp != 0)
            animator.Hited();

        if(hp == 0)
        {
            isDead = true;
            Dead();
            attack.OnKill?.Invoke(this);
        }
    }

    protected virtual Character GetSkillTarget()
    {
        targets.Sort((t1, t2) =>
        {
            var dis1 = GetDisatnce(t1);
            var dis2 = GetDisatnce(t2);
            return dis1.CompareTo(dis2);
        });
        return targets.FirstOrDefault();
    }

    protected float GetDisatnce(Character target)
    {
        return (target.transform.position - transform.position).sqrMagnitude;
    }

    private IEnumerator Attacking()
    {
        float atAttack = -coolTime;
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (Time.time < atAttack + coolTime)
                continue;

            Character target = GetSkillTarget();
            if (target == null)
                continue;

            animator.Attack();
            if (targettingSkill)
                skill.Active(target, OnAttack, OnKill);
            else
                skill.Active(enemyTags, OnAttack, OnKill);

            atAttack = Time.time;
        }
    }

}
