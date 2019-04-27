using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Linq;

public class TowerControlUI : AdvStaticComponent<TowerControlUI> {

    private Canvas canvas;
    private Tower tower;

    [SerializeField] Text movingCost;

    [SerializeField] Button moveButton;

    [SerializeField] Animating_Multi floating;
    [SerializeField] Animating_Multi closing;

    private ControlTower control;
    protected override void Initialize()
    {
        base.Initialize();
        control = ControlTower.instance;
        canvas = GetComponentInChildren<Canvas>();
        _Close();
    }

    public static void Float(Tower tower)
    {
        instance._Float(tower);
    }

    private void _Float(Tower tower)
    {
        canvas.gameObject.SetActive(true);
        floating.Animate();

        movingCost.text = control.movingCost.ToString();

        canvas.transform.position = tower.transform.position;
        this.tower = tower;
    }

    private void Update()
    {
        moveButton.interactable = control.CanMove();
    }

    public void Move()
    {
        control.ReadyMove(tower);
        Close();
    }

    public void Remove()
    {
        control.RemoveTower(tower);
        Close();
    }

    public void Close()
    {
        tower = null;
        closing.Animate();
    }

    private void _Close()
    {
        canvas.gameObject.SetActive(false);
    }
}
