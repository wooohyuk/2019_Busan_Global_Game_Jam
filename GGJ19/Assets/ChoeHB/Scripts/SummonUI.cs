using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonUI : MonoBehaviour {

    [SerializeField] Transform towerUIHolder;
    [SerializeField] TowerUI towerUIPrefab;

    private TowerUI selectedTowerUI;
    private Battle battle;

    private SummonTower summon;

    public void Awake() => Initialize();

    private void Initialize()
    {
        battle = Battle.instance;
        summon = SummonTower.instance;

        TowerUI.OnPointerDown += OnPressTowerUI;
        towerUIPrefab.gameObject.SetActive(false);
        foreach(var tower in summon.towerCards)
        {
            TowerUI instance = Instantiate(towerUIPrefab);
                instance.Initialize(tower);
                instance.transform.SetParent(towerUIHolder, false);
            instance.gameObject.SetActive(true);
        }

    }

    private void OnDestroy()
    {
        TowerUI.OnPointerDown -= OnPressTowerUI;
    }

    private void OnPressTowerUI(TowerUI towerUI)
    {
        if (selectedTowerUI != null)
            selectedTowerUI.Select(false);

        if (!summon.ReadySummon(towerUI.card))
            return;

        towerUI.Select(true);
        selectedTowerUI = towerUI;

        summon.OnEndSummon += DeSelectCard;
    }

    public void DeSelectCard()
    {
        if (selectedTowerUI == null)
            return;

        selectedTowerUI.Select(false);
        selectedTowerUI = null;
        summon.OnEndSummon -= DeSelectCard;
    }

   

}
