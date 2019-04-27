using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAnimator : CharacterAnimator {

    public void Move(bool flag)
    {
        animator.SetBool("Move", flag);
    }
}
