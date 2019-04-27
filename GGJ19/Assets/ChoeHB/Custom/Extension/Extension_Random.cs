using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public static class Extension_Random
{
    public static T Random<T>(this IEnumerable<T> array)
    {
        int rand = UnityEngine.Random.Range(0, array.Count());
        return array.ElementAt(rand);
    }

    public static T Random<T>(this Dictionary<T, int> dic)
    {
        int sum = dic.Values.Sum();
        int rand = UnityEngine.Random.Range(1, sum + 1);
        foreach(var kv in dic)
        {
            rand -= kv.Value;
            if (rand <= 0)
                return kv.Key;
        }
        throw new System.Exception(string.Format("...? Sum = {0}, Rand = {1}", sum, rand));
    }
}
