using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalObjectPool : StaticComponent<GlobalObjectPool>
{
    private class Pool
    {
        public GameObject prefab;
        private Action<GameObject> Constructor;
        private List<GameObject> objects;
        private Transform holder;

        public Pool(GameObject prefab, int prespawnCount, Transform holder, Action<GameObject> Constructor = null)
        {
            this.prefab = prefab;
            this.Constructor = Constructor;
            objects = new List<GameObject>();
            PreSpawn(prespawnCount);
        }

        private void PreSpawn(int count)
        {
            for (int i = 0; i < count; i++)
                Spawn();
        }

        private GameObject Spawn()
        {
            var obj = Instantiate(prefab);
            if (Constructor != null)
                Constructor(obj);
            obj.transform.SetParent(holder);
            objects.Add(obj);
            return obj;
        }

        public GameObject GetPooledObject()
        {
            GameObject toReturn = null;
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i] == null)
                {
                    objects.RemoveAt(i--);
                    continue;
                }

                if (objects[i].activeSelf)
                    continue;
                toReturn = objects[i];
                break;
            }
            toReturn = toReturn ?? Spawn();
            return toReturn;
        }
        
    }

    public static void Clear() => prefabToPool.Clear();
    
    private static Dictionary<GameObject, Pool> prefabToPool = new Dictionary<GameObject, Pool>();

    public static void TryAddPool<T>(T prefab, int prespawnCount, Action<GameObject> Constructor = null) where T : Component
        => TryAddPool(prefab.gameObject, prespawnCount, Constructor);

    public static void TryAddPool(GameObject prefab, int prespawnCount, Action<GameObject> Constructor = null)
    {
        if (prefabToPool.ContainsKey(prefab))
        {
            if (prefabToPool[prefab].prefab == null)
                prefabToPool.Remove(prefab);
            else
                return;
        }

        Transform holder = new GameObject(string.Format("Holder({0})", prefab.name)).transform;
        Pool pool = new Pool(prefab, prespawnCount, holder, Constructor);
        prefabToPool.Add(prefab, pool);
    }

    public static T GetPooledObject<T>(T component) where T : Component
        => GetPooledObject(component.gameObject).GetComponent<T>();

    public static GameObject GetPooledObject(GameObject prefab)
    {
        if (!prefabToPool.ContainsKey(prefab))
            throw new Exception(string.Format("Prefab({0}) isn't Contained", prefab.name));
        return prefabToPool[prefab].GetPooledObject();
    }
}

