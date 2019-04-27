using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Slow : Skill
{
    [Range(0,1f)]
    [SerializeField] float value = 0.4f;
    [SerializeField] float during = 0.5f;

    protected override void _Active(Attack attack, Character target)
    {
        target.Slow(attack, this, value, during);
    }

    protected override void _Active(Attack attack, List<string> targetTags)
    {
        throw new System.NotImplementedException();
    }
}
