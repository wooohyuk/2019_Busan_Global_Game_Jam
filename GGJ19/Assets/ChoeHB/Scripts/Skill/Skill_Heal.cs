using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Heal : Skill
{
    protected override void _Active(Attack attack, Character target)
    {
        target.Heal(attack);
    }

    protected override void _Active(Attack attack, List<string> targetTags)
    {
        throw new System.NotImplementedException();
    }
}
