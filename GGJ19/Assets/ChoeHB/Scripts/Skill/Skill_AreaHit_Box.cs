using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_AreaHit_Box : Skill
{
    [SerializeField] Vector2 size;

    protected override void _Active(Attack attack, Character target)
    {
        throw new System.NotImplementedException();
    }

    protected override void _Active(Attack attack, List<string> targetTags)
    {
        Vector2 size = new Vector2(
            this.size.x * transform.lossyScale.x,
            this.size.y * transform.lossyScale.y
        );

        Collider2D[] hiteds = Physics2D.OverlapBoxAll(transform.position, size, 0);
        for (int i = 0; i < hiteds.Length; i++)
        {
            Collider2D hited = hiteds[i];
            if (!targetTags.Contains(hited.tag))
                continue;
            Character target = hited.GetComponent<Character>();
            target.Hited(attack);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, size);
    }

}