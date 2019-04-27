using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;
using System.Reflection;

public interface ISave
{
    string GetIdentifier();
    JSONObject ToSaveData();
    void FromSaveData(JSONObject json);
}

/* -----------------
 * ConsFromSaveData를 사용하기 위해서는 ISave를 상속받은 클래스가 JSONObject만으로 생성하는 생성자가 있어야 한다.
 * ---------------- */

public static class Extension_ISave
{
    #region dictionary_save 

    public static JSONObject ToSaveData<T>(this Dictionary<string, T> map) where T : ISave
    {
        JSONObject json = new JSONObject();
        foreach (var key in map.Keys)
            json[key] = map[key].ToSaveData();
        return json;
    }

    public static void ConsFromSaveData<T>(this Dictionary<string,T> map, JSONObject json) where T : ISave
    {
        try
        {
            if (map.Count != 0)
                throw new Exception("map.Count != 0");

            var cons = typeof(T).GetConstructors();
            ConstructorInfo conInfo = (from con in cons
                                       where con.GetParameters()[0].ParameterType == typeof(JSONObject)
                                       select con).Single();

            foreach (var key in json.keys)
                map.Add(key, (T)conInfo.Invoke(new object[] { json[key] }));
        }

        catch (Exception e)
        {
            Debug.LogError(e);
            throw e;
        }
    }

    public static void RecoveryFromSaveData<T>(this Dictionary<string, T> map, JSONObject json) where T : ISave
    {
        try
        {
            if (map.Count == 0)
                throw new Exception("map.count == 0");

            foreach(var key in map.Keys)
                map[key].FromSaveData(json[key]);
        }

        catch (Exception e)
        {
            Debug.LogError(e);
            throw e;
        }
    }

    #endregion

    #region (list & array)_save 

    public static JSONObject ToSaveData<T>(this T[] array) where T : ISave
    {
        JSONObject json = JSONObject.arr;
        for (int i = 0; i < array.Length; i++)
        {
            json.Add(array[i].ToSaveData());
        }
        return json;
    }

    public static JSONObject ToSaveData<T>(this List<T> list) where T : ISave
    {
        JSONObject json = JSONObject.arr;
        foreach (var element in list)
            json.Add(element.ToSaveData());
        return json;
    }

    // require T(JSONObject json)
    public static void ConsFromSaveData<T>(this List<T> list, JSONObject json) where T : ISave
    {
        try
        {
            if (list.Count != 0)
                throw new Exception("list.count != 0");

            var cons = typeof(T).GetConstructors();
            ConstructorInfo conInfo = (from con in cons
                                       where con.GetParameters()[0].ParameterType == typeof(JSONObject)
                                       select con).Single();

            foreach (var element in json.list)
                list.Add((T)conInfo.Invoke(new object[] { element }));
        }

        catch (Exception e)
        {
            Debug.LogError(e);
            throw e;
        }
    }

    public static void ConsFromSaveData<T>(this T[] array, JSONObject json) where T : ISave
    {
        try
        {
            if (array.Length != json.Count)
                throw new Exception("array.Length != json.Count");

            var cons = typeof(T).GetConstructors();
            ConstructorInfo conInfo = (from con in cons
                                       where con.GetParameters()[0].ParameterType == typeof(JSONObject)
                                       select con).Single();

            for(int i=0;i<array.Length;i++)
                array[i] = (T)conInfo.Invoke(new object[] { json[i] });
        }

        catch (Exception e)
        {
            Debug.LogError(e);
            throw e;
        }
    }

    public static void RecoveryFromSaveData<T>(this IEnumerable<T> enums, JSONObject json) where T : ISave
    {
        var tmpArray = enums.ToArray();
        for (int i = 0; i < json.Count; i++)
            tmpArray[i].FromSaveData(json[i]);
    }

    #endregion

    public static void AddISave(this JSONObject json, ISave save)
    {
        json.AddField(save.GetIdentifier(), save.ToSaveData());
    }

    public static void Assigned(this ISave save, JSONObject json)
    {
        try
        {
            JSONObject child = json[save.GetIdentifier()];
            if (child == null || child.IsNull)
                throw new Exception("JSON is Null "+save.GetIdentifier());
            save.FromSaveData(child);
        }

        catch (Exception e)
        {
            Debug.LogError(e.StackTrace);
            throw e;
        }
    }
}