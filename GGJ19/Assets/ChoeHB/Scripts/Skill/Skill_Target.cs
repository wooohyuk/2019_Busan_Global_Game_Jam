using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Target : Skill {

    protected override void _Active(Attack attack, Character target)
    {
        target.Hited(attack);
    }

    protected override void _Active(Attack attack, List<string> targetTags)
    {
        throw new System.NotImplementedException();
    }

}
