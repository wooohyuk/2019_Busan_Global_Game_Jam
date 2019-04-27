using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public interface Initializable
{
    void Initialize();
}

public static class CacheObjectManager {

    private static Dictionary<string, object> statics       = new Dictionary<string, object>();
    private static Dictionary<string, object> singletons    = new Dictionary<string, object>();


    /* ------------ Singleton ----------- */
    public static T LoadAsSingleton<T>(string assetName) where T : Component
        => GetInstance<T>(assetName, true, false);

    public static T GetAsSingleton<T>(string assetName) where T : Component
        => GetInstance<T>(assetName, true);
    /* --------------------------------- */


    /* ------------- Static ------------ */
    public static T LoadAsStatic<T>(string assetName) where T : Component
    => GetInstance<T>(assetName, false, false);

    public static T GetAsStatic<T>(string assetName) where T : Component
        => GetInstance<T>(assetName, false);
    /* -------------------------------- */


    public static T GetInstance<T>(string assetName, bool isSingleton, bool activeTrue = true) where T : Component
    {
        var dict = isSingleton ? statics : singletons;
        if (dict.ContainsKey(assetName))
        {
            if (dict[assetName].ToString().Equals("null"))
                dict.Remove(assetName);
            else
                return (T)dict[assetName];
        }

        string createMethod;
        T instance;

        // 만약 인스턴스가 없다면 필드에서 찾아본다.
        var objects = GameObject.FindObjectsOfType<T>();

        // 필드에서 인스턴스가 1개라면 그걸 사용한다.
        if (objects.Length == 1)
        {
            instance = objects[0];
            createMethod = "필드에서 찾음";
        }

        // 필드에서 인스턴스가 2개 이상 발견된다면 잘못된 경우이다.
        // 지우라고 에러를 띄워주고 그냥둔다.
        else if (1 < objects.Length)
        {
            for (int i = 1; i < objects.Length; i++)
                GameObject.Destroy(objects[i].gameObject);
            instance = objects[0];
            createMethod = "필드에서 여러개 찾아서 그 중에 하나를 고름";
        }

        // 필드에서 하나도 나오지 않았다면 생성해서 쓴다.
        else // == if (cands.Length == 0)
        {
            var prefab = Resources.Load<GameObject>(assetName);
            if (prefab != null)
            {
                instance = GameObject.Instantiate(prefab).GetComponent<T>();
                if (instance == null)
                    throw new Exception($"{assetName}에는 {typeof(T)}가 존재하지 않음");
                createMethod = "Resource.Load로 불러옴";
            }

            // 프리팹도 없으면 아예 새로 만든다.
            else
            {
                instance = new GameObject(assetName).AddComponent<T>();
                createMethod = "새로 만듬";
            }
        }

        instance.gameObject.SetActive(activeTrue);

        if (instance is Initializable)
            ((Initializable)instance).Initialize();

        if (isSingleton)
            GameObject.DontDestroyOnLoad(instance.gameObject);

        instance.gameObject.SetActive(activeTrue);
        // Debug.Log(typeof(T).ToString().Fill("Green") + " : " + createMethod);

        dict.Add(assetName, instance);
        return instance;
    }




}
