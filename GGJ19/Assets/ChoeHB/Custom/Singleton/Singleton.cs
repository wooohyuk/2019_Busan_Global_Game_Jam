using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> where T : Singleton<T>, new()
{
    protected virtual void Initialize() { }

    protected static T instance_;
    public static T instance
    {
        get
        {
            if (instance_ == null)
                instance_ = new T();
            return instance_;
        }
    }

    protected Singleton() { }

    protected static void CheckInstance()
    {
        if (instance_ == null)
        {
            instance_ = new T();
            instance_.Initialize();
        }
    }
}
