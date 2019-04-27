using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class SingletonComponent<T> : SerializedMonoBehaviour where T : SingletonComponent<T>{

    public static bool isDestroyed { get; private set; }

    protected virtual void Initialize() { }

    protected static T instance_;
    public static T instance
    {
        get
        {
            if (instance_ == null)
            {
                instance_ = FindObjectOfType<T>();
                if (instance_ == null)
                    throw new System.Exception("Generate] Can't Find " + typeof(T));
                Init(instance_);
            }
            return instance_;
        }
    }

    protected virtual void Awake()
    {
        if (instance_ == null)
            Init(GetComponent<T>());

        if (instance_.gameObject != gameObject)
            Destroy(gameObject);
    }

    protected static void Init(T t)
    {
        DontDestroyOnLoad(t.gameObject);
        instance_ = t;
        instance_.Initialize();
    }

    protected static void CheckInstance()
    {
        if (instance == null)
        {
        }
    }

    protected  virtual void OnDestroy()
    {
        isDestroyed = true;
    }
}
