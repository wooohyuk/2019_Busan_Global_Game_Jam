using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GhostTable : SingletonScriptableObject<GhostTable>
{


#if UNITY_EDITOR

    [MenuItem("테이블/고스트")]
    public static void Select()
    {
        Selection.activeObject = instance;
    }

#endif

    [SerializeField] float hp                = 1;
    [SerializeField] float damage            = 1;
    [SerializeField] float attackSpeed       = 1;
    [SerializeField] float speed             = 1;

    [TableList]
    [SerializeField] List<GhostStatus> statuses;

    public static IEnumerable<string> GetGhostIds() => instance.statuses.Where(s => s.isUsed).Select(s => s.id);
    public static GhostStatus GetStatus(string id)
    {
        foreach(var status in instance.statuses)
        {
            if (status.id != id)
                continue;
            GhostStatus s       = new GhostStatus();
                s.id            = status.id;
                s.isUsed        = status.isUsed;
                s.prefab        = status.prefab;
                s.hp            = (int)(status.hp * instance.hp);
                s.damage        = (int)(status.damage * instance.damage);
                s.attackSpeed   = status.attackSpeed * instance.attackSpeed;
                s.speed         = status.speed * instance.speed;
                s.cost          = status.cost;
                s.name          = status.name;
            s.sprite= status.sprite;
            return s;
        }
        throw new Exception($"{id.ToString().Fill("White")}를 찾을 수 없음");
    }
}