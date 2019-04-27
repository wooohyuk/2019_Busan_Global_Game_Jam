using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour {

    public static event Action<TowerUI> OnPointerDown;

    [SerializeField] Text costText;
    [SerializeField] Image image;
    [SerializeField] Outline outline;
    [SerializeField] GameObject disable;
    [SerializeField] EventTrigger eventTrigger;

    public TowerCard card { get; private set; }

    private bool isInteractable;

    public void Initialize(TowerCard card)
    {
        this.card = card;

        image.sprite = card.sprite;

        eventTrigger.AddListener(EventTriggerType.PointerDown, _ => Interact());
        Select(false);
        costText.text = card.cost.ToString();
    }

    private void Interact()
    {
        if (!isInteractable)
            return;
        OnPointerDown?.Invoke(this);
    }

    public void Select(bool flag)
    {
        transform.localScale = Vector3.one * (flag ? 1.2f : 1f);
    }

    private void Update()
    {
        SetInteractable(SummonTower.instance.CanSummon(card));
    }

    private void SetInteractable(bool flag)
    {
        isInteractable = flag;
        disable.gameObject.SetActive(!flag);
    }

}
