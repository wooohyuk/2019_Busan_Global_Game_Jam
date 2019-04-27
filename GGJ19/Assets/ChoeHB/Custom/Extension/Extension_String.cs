using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public static class Extension_String
{
    public static string Format(this string str, params object[] parms)
    {
        return string.Format(str, parms);
    }

    // a, b, 
    public static string[] SplitTrim(this string str, string pattern)
    {
        return Regex.Split(str, pattern).Select(s => s.Trim()).ToArray();
    }

    public static void SetInt(this Text text, int value)
    {
        text.text = value.ToString();
    }

    public static string SumAsString<T>(this IEnumerable<T> list, string sep = ", ")
    {
        if (list.Count() == 0)
            return "";
        string s = list.Take(1).Single().ToString();
        foreach (var element in list.Skip(1))
            s += sep + element;
        return s;
    }

    public static string SumAsString<T>(this IEnumerable<T> list, char sep = ',') => list.SumAsString(sep.ToString());
}