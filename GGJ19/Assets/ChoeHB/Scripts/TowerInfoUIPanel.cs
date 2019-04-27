using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInfoUIPanel : MonoBehaviour {

    [SerializeField] Transform holder;
    [SerializeField] TowerInfoUI prefab;
    
    private void Awake()
    {
        foreach(var id in TowerTable.GetTowerIds())
        {
            TowerInfoUI instance = Instantiate(prefab);
            instance.transform.SetParent(holder, false);
            instance.Initialize(id);
            instance.gameObject.SetActive(true);
        }
        prefab.gameObject.SetActive(false);
    }

}
