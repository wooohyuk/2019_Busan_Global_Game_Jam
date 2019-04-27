using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension_Enum {

    public static T ParseEnum<T>(this string str)
    {
        string[] names = Enum.GetNames(typeof(T));
        Array values = Enum.GetValues(typeof(T));
        for (int i = 0; i < names.Length; i++)
            if (names[i] == str)
                return (T)values.GetValue(i);
        throw new Exception($"Can't Find {str.Fill("Yellow")}");
    }

}
