using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostManager : AdvStaticComponent<CostManager> {
    
    private SpriteRenderer cursor;

    public float fCost { get; private set; }
    public int cost => (int)fCost;

    public bool CanUse(int cost) => cost <= fCost;

    public void Earn(int cost)
    {
        fCost += cost;
    }

    public void Use(int cost)
    {
        if (!CanUse(cost))
            throw new System.Exception("CanUse " + cost);
        fCost -= cost;
        return;
    }



}
