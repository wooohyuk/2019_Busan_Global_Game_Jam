using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class GhostStatus : Status
{
    public float speed;
    public int cost;
}

public class Ghost : Character
{
    [ValueDropdown(nameof(GetIds))]
    [SerializeField] string id;

    public int cost { get; private set; }
    public string name { get; private set; }
    private IEnumerable<string> GetIds() => GhostTable.GetGhostIds();
    private GhostAnimator gAnimator => (GhostAnimator)animator;

    private Dictionary<object, Tuple<float, float>> slows; // Tuple = (슬로우 수치, 끝나는 시간)

    private float originSpeed;
    private float speed;
    public Sprite sprite { get; private set; }
    
    protected override void Awake()
    {
        base.Awake();
        var status = GhostTable.GetStatus(id);
        name = status.name;
        sprite = status.sprite;
        Initialize(status.hp, status.damage, status.attackSpeed);
        cost = status.cost;
        slows = new Dictionary<object, Tuple<float, float>>();
    }

    protected override void Reset()
    {
        base.Reset();
        if (animator == null)
            animator = GetComponentInChildren<GhostAnimator>();

        if (animator == null)
        {   
            animator = new GameObject("Animator").AddComponent<GhostAnimator>();
            animator.transform.SetParent(transform, false);
        }

        enemyTags.Add("Tower");
        tag = "Ghost";
    }


    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(Moving());
        slows.Clear();
    }

    public override void Slow(Attack attack, object sender, float value, float during)
    {
        attack.OnHit?.Invoke(this);
        var tuple = new Tuple<float, float>(value, Time.time + during);
        if (slows.ContainsKey(sender))
            slows[sender] = tuple;
        else
            slows.Add(sender, tuple);
    }


    protected override void Initialize(int maxHp, int damage, float attackSpeed)
    {
        base.Initialize(maxHp, damage, attackSpeed);
        var status = GhostTable.GetStatus(id);
        originSpeed = speed = status.speed;
    }


    private IEnumerator Moving()
    {
        while (true)
        {
            /* ------------ Slow ------------ */
            yield return new WaitForEndOfFrame();
            if (slows.Count == 0)
                speed = originSpeed;

            else
            {
                foreach (var sender in slows.Keys.ToList())
                {
                    var vt = slows[sender];
                    if (vt.Item2 < Time.time)
                        slows.Remove(sender);
                }

                float slow = 1;
                foreach (var vt in slows.Values)
                {
                    slow *= (1 - vt.Item1);
                }

                speed = originSpeed * slow;
            }
            /* -------------------------- */

            if (isDead)
                speed = 0;

            var near = GetSkillTarget();
            if (near != null && GetDisatnce(near) < 2)
            {
                gAnimator.Move(false);
                continue;
            }
            gAnimator.Move(true);
            
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }

    public void Spawn(int line)
    {
        transform.position = (Vector2)Tile.east[line].transform.position;
        hp = maxHp;
    }

}
