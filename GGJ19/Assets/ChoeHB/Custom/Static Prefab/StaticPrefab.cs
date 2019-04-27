using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 어디서든 instance로 접글할 수 있지만
// 존재하지 않는다면 Resources.Load를 통해 읽어와서 캐싱한다.

public abstract class StaticPrefab<T> : MonoBehaviour where T : StaticPrefab<T>
{
    private static T instance_;
    public static T instance {
        get
        {
            // 이미 캐싱되어 있다면 반환한다.
            if (instance_ != null)
                return instance_;

            instance_ = FindObjectOfType<T>();

            if (instance_ == null)
                instance_ = Instantiate(Resources.Load<T>("Prefabs/"+typeof(T).Name));
            
            if (!instance_.isInitialized)
            {
                instance_.Initialize();
                instance_.isInitialized = true;
            }

            return instance_;
        }
    }

    protected virtual void Initialize() { }
    private bool isInitialized;

    protected virtual void Awake()
    {
        if (!isInitialized)
        {
            Initialize();
            isInitialized = true;
        }
    }
}
