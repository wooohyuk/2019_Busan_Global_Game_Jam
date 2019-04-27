using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Battle : StaticComponent<Battle> {

    public static event Action OnStart;

    public List<Tower> towers   { get; private set; }

    private TowerCard selectedCard;
    private SpriteRenderer cursor;
    private bool isStarted;

    private void Awake()
    {
        towers = new List<Tower>();
        cursor = new GameObject("Curcor").AddComponent<SpriteRenderer>();
        cursor.transform.SetParent(transform);
        StartBattle();
    }

    [Button]
    public void StartBattle()
    {
        if (isStarted)
            return;
        isStarted = true;
        OnStart?.Invoke();
    }

    public Tower GetTowerAsTile(Tile tile)
        => towers.SingleOrDefault(t => t.tile == tile);

    public void AddTower(Tower tower)
    {
        towers.Add(tower);
    }

    public void RemoveTower(Tower tower)
    {
        towers.Remove(tower);
    }

}
