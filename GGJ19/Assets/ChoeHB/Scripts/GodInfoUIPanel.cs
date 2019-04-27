using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodInfoUIPanel : MonoBehaviour {
    
    [SerializeField] Transform holder;
    [SerializeField] GodInfoUI prefab;

    private void Awake()
    {
        foreach(var id in TowerTable.GetTowerIds())
        {
            var instance = Instantiate(prefab);
                instance.transform.SetParent(holder, false);
            instance.gameObject.SetActive(true);
            instance.SetId(id);
        }
        prefab.gameObject.SetActive(false);
    }
}
