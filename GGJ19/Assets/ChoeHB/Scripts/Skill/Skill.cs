using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Skill : SerializedMonoBehaviour
{

    [SerializeField] bool useEffect;
    [SerializeField] float preDelay;

    [ShowIf(nameof(useEffect))]
    [SerializeField] string effectName;

    [ShowIf(nameof(useEffect))]
    [SerializeField] float effectScale = 1;

    private Character _owner;
    protected Character owner
    {
        get
        {
            if (_owner == null)
                _owner = GetComponentInParent<Character>();
            return _owner;
        }
    }

    // 단일대상 공격
    public void Active(Character target, Action<Character> OnHit = null, Action<Character> OnKill = null)
    {
        int damage = owner.damage;
        Attack attack = new Attack(damage);
            attack.OnHit    = OnHit;
            attack.OnKill   = OnKill;

        if (useEffect)
            attack.OnHit += t => EffectAnimator.Play(effectName, t.transform.position);

        Action act = () => _Active(attack, target);
        if (preDelay == 0)
            act();
        else
            StartCoroutine(act.After(preDelay));

    }

    // 광역 공격
    public void Active(List<string> targetTags, Action<Character> OnHit = null, Action<Character> OnKill = null)
    {
        int damage = owner.damage;
        Attack attack = new Attack(damage);
            attack.OnHit    = OnHit;
            attack.OnKill   = OnKill;

        if (useEffect)
            attack.OnHit += t => EffectAnimator.Play(effectName, t.transform.position, effectScale);


        Action act = () => _Active(attack, targetTags);
        if (preDelay == 0)
            act();
        else
            StartCoroutine(act.After(preDelay));
    }

    // 단일
    protected abstract void _Active(Attack attack, Character target);

    // 광역
    protected abstract void _Active(Attack attack, List<string> targetTags);

}