using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalObjectPool<T> where T : Component {

    private List<T> objects;
    private T prefab;
    private Action<T> Constructor;
    private Action<T> OnSpawn;

    private Transform holder;

    public LocalObjectPool(T prefab, Transform holder, int preSpawnCount = 0, Action<T> Constructor = null, Action<T> OnSpawn = null)
    {
        this.prefab = prefab;
        this.Constructor    = Constructor;
        this.OnSpawn        = OnSpawn;
        this.holder         = holder;

        objects = new List<T>();
        PreSpawn(preSpawnCount);
    }

    private void PreSpawn(int count)
    {
        for (int i = 0; i < count; i++)
            Spawn();
    }

    private T Spawn()
    {
        var obj = GameObject.Instantiate(prefab);
        if (Constructor != null)
            Constructor(obj);
        objects.Add(obj);
        obj.transform.SetParent(holder);
        obj.gameObject.SetActive(false);
        return obj;
    }

    private T Spawn(Vector3 position)
    {
        var obj = GameObject.Instantiate(prefab, position, Quaternion.identity);
        if (Constructor != null)
            Constructor(obj);
        objects.Add(obj);
        return obj;
    }

    public T GetPooledObject(Vector3 position)
    {
        T t = null;

        for (int i = 0; i < objects.Count; i++)
        {
            var obj = objects[i];
            if (obj.gameObject.activeSelf)
                continue;
            t = obj;
            break;
        }

        if (t == null)
            t = Spawn(position);

        t.gameObject.SetActive(false);
        t.transform.position = position;
        t.gameObject.SetActive(true);
        if (OnSpawn != null)
            OnSpawn(t);

        return t;
    }

    public T GetPooledObject()
    {
        T t = null;

        for (int i = 0; i < objects.Count; i++)
        {
            var obj = objects[i];
            if (obj.gameObject.activeSelf)
                continue;
            t = obj;
            break;
        }

        if (t == null)
            t = Spawn();

        t.gameObject.SetActive(false);
        t.gameObject.SetActive(true);
        if (OnSpawn != null)
            OnSpawn(t);

        return t;
    }
}
