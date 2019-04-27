using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TowerStatus : Status
{
    public int cost;
}


public class TowerTable : SingletonScriptableObject<TowerTable>
{


#if UNITY_EDITOR

    [MenuItem("테이블/타워")]
    public static void Select()
    {
        Selection.activeObject = instance;
    }

#endif

    [SerializeField] float hp                = 1;
    [SerializeField] float damage            = 1;
    [SerializeField] float attackSpeed       = 1;

    [TableList]
    [SerializeField] List<TowerStatus> statuses;

    public static IEnumerable<string> GetTowerIds() => instance.statuses.Where(s => s.isUsed).Select(s => s.id);
    public static TowerStatus GetStatus(string id)
    {
        foreach (var status in instance.statuses)
        {
            if (status.id != id)
                continue;
            TowerStatus s       = new TowerStatus();
                s.id            = status.id;
                s.isUsed        = status.isUsed;
                s.prefab        = status.prefab;
                s.hp            = (int)(status.hp * instance.hp);
                s.damage        = (int)(status.damage * instance.damage);
                s.cost          = status.cost;
                s.sprite        = status.sprite;
                s.attackSpeed   = status.attackSpeed * instance.attackSpeed;
                s.name          = status.name;
            return s;
        }
        throw new Exception($"{id.ToString().Fill("White")}를 찾을 수 없음");
    }

}