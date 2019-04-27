using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translator : Singleton<Translator>
{
    public static event Action OnUpdateLangauge;
    private static string _language;
    public static string language
    {
        get {
            if (_language == null)
                CheckInstance();
            return _language;
        }
        set {
            _language = value;
            PlayerPrefs.SetString("Language", language);
            textSet = TranslateTable.GetTextSet(language);
            OnUpdateLangauge?.Invoke();
        }
    }

    private static Dictionary<string, string> textSet;

    [ValueDropdown("GetLanguages")]
    [OnValueChanged("UpdateLanguage")]
    [SerializeField] string debugLangauge;

    public Translator()
    {
        instance_ = this;
        string language = PlayerPrefs.GetString("Language", null);

        // 기존에 설정된 언어가 없다면 시스템언어를 불러온다.
        // 시스템언어가 번역테이블에 존재하지 않다면 영어를 쓴다.
        if (string.IsNullOrEmpty(language))
            language = Application.systemLanguage.ToString();

        if (!GetLanguages().Contains(language))
            language = "English";

        Translator.language = language;
    }
    
    public static bool HasId(string id)
    {
        CheckInstance();
        return textSet.ContainsKey(id.ToLower());
    }

    public static string GetText(string id)
    {
        CheckInstance();
        id = id.ToLower();
        if (!textSet.ContainsKey(id))
        {
            Debug.LogWarning("Can't Find :" + id);
            return "ERROR_" + id;
        }
        return textSet[id];
    }

    public static List<string> GetLanguages()
    {
        CheckInstance();
        return TranslateTable.GetLanguages();
    }

}
