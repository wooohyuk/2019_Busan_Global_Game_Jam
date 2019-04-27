using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    public Action<Character> OnHit;
    public Action<Character> OnKill;

    public int damage;

    public Attack(int damage, Action<Character> OnHit = null, Action<Character> OnKill = null)
    {
        this.damage = damage;
        this.OnHit  = OnHit;
        this.OnKill = OnKill;
    }
}
