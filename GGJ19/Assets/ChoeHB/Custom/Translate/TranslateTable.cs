using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TranslateTable : SingletonScriptableObject<TranslateTable> {

    public static event Action OnUpdateTable;

#if UNITY_EDITOR
    [MenuItem("Table/Translate")]
    static void Select()
    {
        Selection.activeObject = instance;
    }
#endif

    [Serializable]
    private class KV {
        [ReadOnly] public string id;
        [ReadOnly] public string translated;
    }

    /* --------- Json ----------- */
    [FolderPath]
    [SerializeField] string jsonPath;

    private string jsonText;

    [HideInInspector]
    [SerializeField] JSONObject json;
    /* ------------------------ */

    [OnValueChanged(nameof(Search))]
    [ValueDropdown("GetLanguages")]
    [SerializeField] string language;

    [OnValueChanged(nameof(Search))]
    [SerializeField] bool showAllSheets;

    [HideIf("showAllSheets")]
    [ValueDropdown("GetSheetNames")]
    [OnValueChanged("UpdateSearched")]
    [SerializeField] string sheetName;

    //[OnValueChanged(nameof(UpdateSearched))]
    [HideLabel] [Header("Search")]
    [SerializeField] string inputField;

    [TableList]
    [SerializeField] List<KV> searchedDatas;

    [HideInInspector]
    [SerializeField] Dictionary<string, Dictionary<string, string>> allDatas;

    public static IEnumerable<string> GetIds() => instance.allDatas.Values.ElementAt(0).Keys;

    [Button]
    private void UpdateJSONText()
    {
        string path = jsonPath + "/Translate.txt";

        StreamReader reader = new StreamReader(path);
        jsonText = reader.ReadToEnd();
        UpdateJSON();
        reader.Close();
    }

    private void UpdateJSON()
    {
        string unescapedText = Regex.Unescape(jsonText);
        jsonText = unescapedText;
        json = new JSONObject(jsonText);
        language = GetLanguages()[0];

        if(string.IsNullOrEmpty(sheetName))
            sheetName = json[language].keys[0];

        Search();

        allDatas = new Dictionary<string, Dictionary<string, string>>();
        foreach(var language in GetLanguages())
        {
            var data = new Dictionary<string, string>();
            foreach (var sheet in GetSheetNames())
                foreach (var key in json[language][sheet].keys)
                    data.Add(key.ToLower(), json[language][sheet][key].str);
            allDatas.Add(language, data);
        }

        OnUpdateTable?.Invoke();
        Translator.language = Translator.language;
    }

    private List<string> GetSheetNames()
    {
        return json[language].keys;
    }

    public static List<string> GetLanguages()
    {
        return instance.json.keys;
    }

    [Button]
    private void Search()
    {
        searchedDatas = new List<KV>();

        if (showAllSheets)
        {
            foreach (var sheetName in GetSheetNames())
                AddElements(searchedDatas, sheetName);
        }
        else
            AddElements(searchedDatas, sheetName);
        if (!string.IsNullOrEmpty(inputField))
            searchedDatas = searchedDatas.Where(d => d.id.Contains(inputField)).ToList();
    }

    private void AddElements(List<KV> datas, string sheetName)
    {
        foreach (string key in json[language][sheetName].keys)
        {
            string value = json[language][sheetName][key].str;
            searchedDatas.Add(new KV()
            {
                id = key.ToLower(),
                translated = value
            });
        }
    }

    public static Dictionary<string, string> GetTextSet(string language) => instance.allDatas[language];

}
