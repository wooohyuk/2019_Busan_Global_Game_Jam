using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

/*
 * 희망사항
 * 
 * 강제 저장 해줘야함...
 * 
 * 언어와 id를 이용한 2차원 테이블
 *  - FontUpdater의 Database
 *  - id별로 default font를 설정해 줘야함.
 *  
 * ex)
 *          |      lobby_title      |      lobby_content     |
 * -----------------------------------------------------------
 * default  |    나눔스퀘어_bold    |     나눔스퀘어_light   |
 * 한국어   |    나눔스퀘어_bold    |     나눔스퀘어_light   |
 * 영어     |     orbitron_black    |      orbitron_light    |
 * 
 * 
 * key를 두 개를 사용하는 2차원 테이블을 인스펙터로 표시하기 힘드니 따로 표시
 *  - key별로 폰트를 검색할 수 있어야함.
 *  - 언어별로 폰트를 검색할 수 있어야함
 *  
 * 언어는 여기서 따로 추가하는 게 아니라 TranslateTable에서 결정함.
 * FontUpdater <-> FontTable 바로 오고 갈 수 있으면 좋음.
 *
 */

public class FontTable : SingletonScriptableObject<FontTable>
{


    #if UNITY_EDITOR
    private static UnityEngine.Object prevSelected;

    [MenuItem("번역/폰트")]
    public static void Select()
    {
        prevSelected = Selection.activeObject;
        Selection.activeObject = instance;
    }

    [ShowIf(nameof(prevSelected))]
    [Button]
    public static void Back()
    {
        if (prevSelected == null)
            return;
        Selection.activeObject = prevSelected;
        prevSelected = null;
    }
#endif

    [SerializeField] Font defaultFont;

    // langauge => id => Font
    [HideInInspector] [SerializeField]
    private Dictionary<string, Dictionary<string, Font>> data
        = new Dictionary<string, Dictionary<string, Font>>();

    [SerializeField] [HideInInspector] string language_ = "default";
    [ShowInInspector] [ValueDropdown(nameof(GetLanguages),FlattenTreeView = true)]
    private string language {
        get { return language_; }
        set {
            if (idToFont != null && idToFont.Count != 0)
                ForceSave();
            language_ = value;
            idToFont = new List<Font>();
            foreach (var id in ids)
                idToFont.Add(GetFont(language, id));
        }
    }

    private IEnumerable<string> GetLanguages()
    {
        yield return "default";
        foreach (var language in Translator.GetLanguages())
            yield return language;
    }

    [HorizontalGroup("G", width : 140)]
    [ListDrawerSettings(
        CustomAddFunction = nameof(AddId),
        CustomRemoveElementFunction = nameof(RemoveId),
        Expanded = true)]
    [SerializeField] List<string> ids;

    // ViewMode가 Id일때 사용
    [HorizontalGroup("G")]
    [ListDrawerSettings(Expanded = true, HideRemoveButton = true, HideAddButton = true)]
    [ShowInInspector] List<Font> idToFont;



    /* ---------- Static -------- */
    public static Font GetFont(string id)
    {
        string language = Translator.language;

        var font = instance.GetFont(language, id);

        if (font == null)
            font = instance.GetFont("default", id);

        if (font == null)
            font = instance.defaultFont;

        return font;
    }

    public static IEnumerable<string> GetIds() => instance.ids;
    /* -----------------------*- */

    private void AddId()
    {
        ids.Add(null);
        idToFont.Add(null);
    }

    private void RemoveId(string id)
    {
        int index = ids.IndexOf(id);
        idToFont.RemoveAt(index);
        ids.RemoveAt(index);
    }

    
    private Font GetFont(string language, string id)
    {
        if (!data.ContainsKey(language))
            data.Add(language, new Dictionary<string, Font>());

        var set = data[language];
        if (!set.ContainsKey(id))
            set.Add(id, null);

        return set[id];
    }

    protected override void Initialize() {
        AddId();
        ids[0] = "default";
        language = Translator.GetLanguages().SingleOrDefault();
        ForceSave();
    }

    protected override void Update()
    {
        language = language;
    }

    [Button]
    private void ForceSave()
    {
        for (int i = 0; i < ids.Count; i++)
        {
            string id = ids[i];
            if (!data.ContainsKey(language))
                data.Add(language, new Dictionary<string, Font>());

            var set = data[language];
            if (!set.ContainsKey(id))
                set.Add(id, null);

            set[id] = idToFont[i];
        }
    }

}