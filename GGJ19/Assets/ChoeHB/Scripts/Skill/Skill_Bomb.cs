using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Bomb : Skill_AreaHit_Box
{
    [SerializeField] string selfBombId;
    [SerializeField] float selfBombScale = 3;

    protected override void _Active(Attack attack, Character target)
    {
        throw new System.NotImplementedException();
    }

    protected override void _Active(Attack attack, List<string> targetTags)
    {
        Attack self = new Attack(9999);
            self.OnHit += t => EffectAnimator.Play(selfBombId, transform.position, selfBombScale);

        owner.Hited(self);
        base._Active(attack, targetTags);
    }
}
