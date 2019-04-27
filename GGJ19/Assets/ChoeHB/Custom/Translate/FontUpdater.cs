using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Translator의 언어가 변경되면 알아서 폰트를 바꿉니다.
/// FontTable에서 id에 해당하는 폰트를 불러옴 (Global Setting)

[ExecuteInEditMode]
[RequireComponent(typeof(Text))]
public class FontUpdater : MonoBehaviour {

    [SerializeField] [HideInInspector]
    private Text _text;
    public Text text {
        get {
            if (_text == null)
                _text = GetComponent<Text>();
            return _text;
        }
    }

    [ValueDropdown(nameof(GetIds))]
    [SerializeField] string id = "default";
    private IEnumerable<string> GetIds() => FontTable.GetIds();

    [ValueDropdown(nameof(GetLanguages))]
    [ShowInInspector]
    private string langauge
    {
        get { return Translator.language; }
        set { Translator.language = value; }
    }

    private IEnumerable<string> GetLanguages() => Translator.GetLanguages();


    private void Awake()
    {
        Translator.OnUpdateLangauge += UpdateFont;
    }

    private void OnDestroy()
    {
        Translator.OnUpdateLangauge -= UpdateFont;
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Application.isPlaying)
            return;
        UpdateFont();
    }

    [Button]
    private void GoFontTable()
    {
        FontTable.Select();
    }
#endif

    private void UpdateFont() {
        if (string.IsNullOrEmpty(id))
            return;
        text.font = FontTable.GetFont(id);
    }

}
