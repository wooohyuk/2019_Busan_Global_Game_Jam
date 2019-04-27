using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower_S : Tower {

    protected override Character GetSkillTarget()
    {
        targets.Sort((t1, t2) =>
        {
            return t2.transform.position.x.CompareTo(t1.transform.position.x);
        });
        return targets.FirstOrDefault();
    }

}
