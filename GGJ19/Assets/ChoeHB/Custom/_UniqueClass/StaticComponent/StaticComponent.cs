using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticComponent<T> : SerializedMonoBehaviour where T : StaticComponent<T> {

    private static T instance_;
    public static T instance
    {
        get
        {
            if (instance_ == null)
                instance_ = FindObjectOfType<T>();

            return instance_;
        }
    }

    protected static void CheckInstance()
    {
        if (instance == null)
        {

        }
    }
}
