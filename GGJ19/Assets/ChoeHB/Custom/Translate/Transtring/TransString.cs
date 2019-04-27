using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using UnityEngine;

[Serializable]
public struct TransString
{
    public string translated { get; private set; }
    public string form { get; private set; }

    private Dictionary<string, Func<object>> paramGetters;

    public TransString(string form)
    {
        if (string.IsNullOrEmpty(form))
            throw new Exception("form은 null로 설정하면 안 됨");

        this.form = form;
        translated = "";
        paramGetters = new Dictionary<string, Func<object>>();
    }

    public void SetForm(string form) => this.form = form;
    public void AddParam(string key, Func<object> valueGetter) => paramGetters.Add(key, valueGetter);

    public string GetText()
    {
        //try
        //{
        Regex regex = new Regex(@"{(?<param>[\w\s]+)}");
        // matches = ["{name}", "{age}"]

        translated = form;
        MatchCollection matches = regex.Matches(form);
        foreach (Match match in matches)
        {
            GroupCollection gc = match.Groups;
            string paramKey = gc["param"].Value;
            if (!paramGetters.ContainsKey(paramKey))
            {
                Debug.LogError($"{paramKey} not in {form}");
                continue;
            }
            string paramValue = paramGetters[paramKey]().ToString();
            translated = Regex.Replace(translated, match.Value, paramValue);
        }
        //정규표현식, Reguler Expression

        return translated;
        //}

        //catch (Exception e)
        //{
        //    Debug.Log("--- Exception ---");
        //    Debug.Log(form);
        //    Debug.LogError(e);
        //    Debug.LogError(e.StackTrace);
        //    Debug.Log("----------------");
        //    return $"TS_ERROR_{form}";
        //}

    }

    public static implicit operator string(TransString ts)
    {
        return ts.GetText();
    }

    public override string ToString()
    {
        return GetText();
    }
}


