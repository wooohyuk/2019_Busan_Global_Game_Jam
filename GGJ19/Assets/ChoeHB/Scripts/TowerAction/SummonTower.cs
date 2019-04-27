using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SummonTower : AdvStaticComponent<SummonTower>
{
    public event Action OnEndSummon;

    public List<TowerCard> towerCards   { get; private set; }

    [SerializeField] SpriteRenderer cursor;
    [SerializeField] AttackRangeUI range;

    private Dictionary<TowerCard, Tower> prefabs;
    private Dictionary<TowerCard, LocalObjectPool<Tower>> towerPools;

    private TowerCard ready;
    private Tile highlighted;

    private bool readySummon;

    public bool CanSummon(TowerCard card) => CostManager.instance.CanUse(card.cost);

    public void SetTowerCount(int towerCount)
    {
        towerCards = new List<TowerCard>();
        
        foreach (var id in TowerTable.GetTowerIds().Take(towerCount))
        {
            TowerCard card = new TowerCard();
            var status = TowerTable.GetStatus(id);
            card.id = id;
            card.prefab = (Tower)status.prefab;
            card.sprite = status.sprite;
            card.cost = status.cost;

            towerCards.Add(card);
        }

        prefabs = new Dictionary<TowerCard, Tower>();
        towerPools = new Dictionary<TowerCard, LocalObjectPool<Tower>>();
        foreach (var card in towerCards)
        {
            Transform towerHolder = new GameObject($"Tower Pool ({card.id})").transform;
            LocalObjectPool<Tower> pool = new LocalObjectPool<Tower>(
                card.prefab, towerHolder, 5
            );
            prefabs.Add(card, card.prefab);
            towerPools.Add(card, pool);
        }
        cursor.gameObject.SetActive(false);
    }

    private Sight GetRange(TowerCard card) => prefabs[card].range;

    public bool ReadySummon(TowerCard tower) => ReadySummon(tower, (x, y) => true);

    // 소환 준비
    public bool ReadySummon(TowerCard tower, Func<int, int, bool> enableChecker)
    {
        if (!CostManager.instance.CanUse(tower.cost))
            return false;

        range.SetSight(GetRange(tower));
        ready = tower;
        cursor.sprite = tower.sprite;
        cursor.gameObject.SetActive(true);

        Tile.SetStateGetter(TileStateGetter);

        //Tile.OnPointerUp    += Summon;
        Tile.OnPointerEnter += HighlightTile;
        Tile.OnPointerExit  += DeHighlightTile;

        readySummon = true;
        Tile.Enable(enableChecker);

        return true;
    }

    private void HighlightTile(Tile tile)
    {
        range.transform.position = tile.transform.position;
        range.Float();
        highlighted = tile;
    }

    private void DeHighlightTile(Tile tile)
    {
        range.Close();
        if (highlighted == tile)
            highlighted = null;
    }

    private Tile.State TileStateGetter(Tile tile)
    {
        if(tile == highlighted)
        {
            if (Battle.instance.towers.Any(t => t.tile == tile))
                return Tile.State.Warning;
            else
                return Tile.State.Highlight;
        }

        else
            return Tile.State.Normal;
    }

    private void Update()
    {
        if (readySummon)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (highlighted != null)
                    Summon(highlighted);

                else
                    Cancle();
            }

            if (highlighted == null)
                cursor.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            else
                cursor.transform.position = highlighted.transform.position;
        }
    }

    // 소환 성공
    public void Summon(Tile tile)
    {
        // 해당 위치에 타워가 있으면
        if (Battle.instance.towers.Any(t => t.tile == tile))
        {
            Cancle();
            return;
        }

        Tower tower = towerPools[ready].GetPooledObject();
        CostManager.instance.Use(ready.cost);

        tower.OnKill += OnKill;
        tower.OnDead += OnDeadTower;
        tower.Summon(tile);
        Battle.instance.AddTower(tower);

        EndSummon();
    }

    private void OnKill(Character target)
    {
        Ghost ghost = (Ghost)target;
        CostManager.instance.Earn(ghost.cost);
    }

    private void OnDeadTower(Character tower)
    {
        Battle.instance.RemoveTower((Tower)tower);
        tower.OnKill -= OnKill;
        tower.OnDead -= OnDeadTower;
    }

    // 소환 취소
    public void Cancle()
    {
        EndSummon();
    }

    private void EndSummon()
    {
        range.Close();
        cursor.gameObject.SetActive(false);
        highlighted = null;
        readySummon = false;
        Tile.Disable();
        Tile.RemoveStateGetter();
        //Tile.OnPointerUp    -= Summon;
        Tile.OnPointerEnter -= HighlightTile;
        Tile.OnPointerExit  -= DeHighlightTile;
        OnEndSummon?.Invoke();
    }
}
