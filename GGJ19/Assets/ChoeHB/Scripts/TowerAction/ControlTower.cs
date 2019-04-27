using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControlTower : AdvStaticComponent<ControlTower> {

    [SerializeField] int m_movingCost = 15;
    [SerializeField] SpriteRenderer cursor;

    public int movingCost => m_movingCost;

    private Tower selectedTower;
    private Tile highlightedTile;

    private bool isMoving;

    private CostManager cm;
    protected override void Initialize()
    {
        base.Initialize();
        Tile.OnPointerDown += SelectTile;
        cm = CostManager.instance;
    }

    private void SelectTile(Tile tile)
    {
        if(EventSystem.current.IsPointerOverGameObject())
            return;

        Tower tower = Battle.instance.GetTowerAsTile(tile);

        if (tower == null)
            return;

        TowerControlUI.Float(tower);
    }

    public bool CanMove() => cm.CanUse(movingCost);
    public void ReadyMove(Tower tower)
    {
        if (!CanMove())
            return;

        cursor.sprite = tower.sprite;
        selectedTower = tower;

        tower.ReadyMove();

        Tile.Enable();

        isMoving = true;
        Tile.SetStateGetter(TileStateGetter);
        Tile.OnPointerDown  -= SelectTile;
        Tile.OnPointerEnter += HighlightTile;
        Tile.OnPointerExit  += DeHighlightTile;
    }
    
    private Tile.State TileStateGetter(Tile tile)
    {
        if (tile == highlightedTile)
        {
            if (Battle.instance.towers.Any(t => t.tile == tile))
                return Tile.State.Warning;
            else
                return Tile.State.Highlight;
        }

        else
            return Tile.State.Normal;
    }

    private void HighlightTile(Tile tile)
    {
        highlightedTile = tile;
        cursor.transform.position = tile.transform.position;
    }

    private void DeHighlightTile(Tile tile)
    {
        if (highlightedTile == tile)
            highlightedTile = null;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(isMoving)
                Submit(highlightedTile);
        }
    }

    private void Submit(Tile tile)
    {
        Tower tower = Battle.instance.GetTowerAsTile(tile);
        if (tower != null)
            return;
        Submit();
    }

    private void Submit()
    {
        if (TileStateGetter(highlightedTile) == Tile.State.Warning)
            throw new System.Exception("설치 못함");
        
        Tile.RemoveStateGetter();
        isMoving = false;
        cm.Use(movingCost);
        selectedTower.MoveTo(highlightedTile);
        MoveEnd();
        cursor.sprite = null;
    }

    private void MoveEnd()
    {
        Tile.Disable();
        Tile.OnPointerDown  += SelectTile;
        Tile.OnPointerDown  -= Submit;
        Tile.OnPointerEnter -= HighlightTile;
        Tile.OnPointerExit  -= DeHighlightTile;
    }

    // 타워 제거
    public void RemoveTower(Tower tower)
    {
        tower.gameObject.SetActive(false);
        Battle.instance.RemoveTower(tower);
    } 

}
