using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TowerAnimator : CharacterAnimator {

    [SerializeField] float moveReadyAlpha = 0.75f;

    public void ReadyMove()
    {
        sr.Alpha(moveReadyAlpha);
    }

    public void EndMove()
    {
        sr.Alpha(1);
    }

}
