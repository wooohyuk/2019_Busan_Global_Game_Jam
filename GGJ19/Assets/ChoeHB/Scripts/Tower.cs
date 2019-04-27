using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Character {

    [SerializeField] AudioClip spawnSound;

    [ValueDropdown(nameof(GetIds))]
    [SerializeField] string id;
    private IEnumerable<string> GetIds() => TowerTable.GetTowerIds();

    public static Tower moveReady;

    public Tile tile { get; private set; }

    private TowerAnimator tAnimator => (TowerAnimator)animator;
    public Sprite sprite => TowerTable.GetStatus(id).sprite;

    protected override void Awake()
    {
        base.Awake();
        TowerStatus status = TowerTable.GetStatus(id);
        Initialize(status.hp, status.damage, status.attackSpeed);
    }

    protected override void Reset()
    {
        base.Reset();
        enemyTags.Add("Ghost");
        tag = "Tower";
        animator = GetComponentInChildren<TowerAnimator>();
        if(animator == null)
        {
            animator = new GameObject("Animator").AddComponent<TowerAnimator>();
            animator.transform.SetParent(transform);
        }
    }

    public void Summon(Tile tile)
    {
        if (spawnSound != null)
            AudioManager.PlaySound(spawnSound);
        MoveTo(tile);
    }
    
    public void ReadyMove()
    {
        tile = null;
        tAnimator.ReadyMove();
    }

    public void MoveTo(Tile tile)
    {
        this.tile = tile;
        transform.position = (Vector2)tile.transform.position;
        EndMove();
    }

    public void CancleMove()
    {
        EndMove();
    }

    private void EndMove()
    {
        tAnimator.EndMove();
    }

    public void Remove()
    {
        gameObject.SetActive(false);
    }

}
