using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostUI : MonoBehaviour {

    [SerializeField] Text text;

    private CostManager cm;

    private void Awake() => cm = CostManager.instance;

    private void Update()
    {
        text.text = $"{cm.cost}";

    }


}
