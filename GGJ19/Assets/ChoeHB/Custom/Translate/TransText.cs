using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(Text))]
[RequireComponent(typeof(FontUpdater))]
public class TransText : MonoBehaviour
{

    [SerializeField]
    [HideInInspector]
    private Text _text;
    public Text text
    {
        get
        {
            if (_text == null)
                _text = GetComponent<Text>();
            return _text;
        }
    }

    /*
     * id를 사용하면 Translator로부터 읽어온 값을 세팅한다.
     *  - id는 useId로 인스펙터에서 설정해주거나, SetId로 외부에서 설정해준다.
     *  - 여기서 AddParam을 하면 그 읽어온 값을 Form으로 쓴다.
     *  
     * Form을 정해줄 때는
     *  - id를 설정해준 뒤 AddParam을 하거나
     *  - SetForm을 이용해서 설정해준다.
     *  
     * 인스펙터에서 id나 form을 설정해주는 경우에는 text로 바로 확인할 수 있음.
     *  
     */ 

    /* -------- 기본 text 설정에 Id를 사용할 때 -------- */
    [SerializeField] bool useId;
    [SerializeField] [HideInInspector]
    private string _id;

    [ValueDropdown(nameof(GetIds))]
    [ShowInInspector] [ShowIf(nameof(useId))]
    private string id
    {
        get { return _id; }
        set
        {
            if (_id == value)
                return;
            _id = value;
            if (translated != null)
                ts.SetForm(translated);
            ForceUpdate();
        }
    }

    private IEnumerable<string> GetIds() => TranslateTable.GetIds();
    /* -------------------------------- */

    /* -------- 인스펙터에서 Form을 사용할 때 --------- */
    //[OnValueChanged(nameof(_SetDefaultForm))]
    [SerializeField] bool useCustomForm;    // 인스펙터에서 form을 정할 때
    //private void _SetDefaultForm() => customForm = " ";

    [SerializeField][HideInInspector]
    private string _customForm = " ";

    [ShowInInspector] [ShowIf(nameof(useCustomForm))]
    [ShowIf(nameof(useCustomForm))]
    private string customForm {
        get { return _customForm; }
        set {
            _customForm = value;
            ts = new TransString(value);
            ForceUpdate();
        }
    }
    /* ------------------------------------------------ */

    private bool useForm;
    private TransString ts;
    [ShowInInspector]
    private string translated
    {
        get
        {
            if (useForm || useCustomForm)
                return ts.ToString();

            if (!useId)
            {
                if(Application.isPlaying)
                {
                    Debug.Log(name);
                    Debug.LogError("문제 | id도 설정 안 하고, form도 설정 안 함");
                    Debug.LogError("해결 | 그러면 대체 왜 만든걸까..");
                }
                return "ERROR";
            }

            if (string.IsNullOrEmpty(id))
            {
                if (Application.isPlaying)
                {
                    Debug.Log(name);
                    Debug.LogError("문제 | id가 Null임. " + name);
                    Debug.LogError("해결 | id를 설정해주거나 form을 설정해줘야함.");
                }
                return "NULL";
            }

            if (!Translator.HasId(id))
            {
                if (Application.isPlaying)
                {
                    Debug.Log(name);
                    Debug.LogError("문제 | id에 해당하는 번역 값이 존재하지 않음(" + id + ")");
                    Debug.LogError("해결 | id를 사용하지 않거나, id에 해당하는 번역텍스트를 추가해야함");
                }
                return $"NULL_{id}";
            }
            return Translator.GetText(id);
        }
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Application.isPlaying)
            return;

        if(useId)
            ForceUpdate();
    }
#endif

    private void Awake()
    {
        Translator.OnUpdateLangauge += ForceUpdate;
        if (useCustomForm)
            ts.SetForm(customForm);
    }

    private void OnDestroy() => Translator.OnUpdateLangauge -= ForceUpdate;

    private void Start()
    {
        // Form을 설정한 경우에는
        // 원하는 시점에 ForceUpdate하지 않으면,
        // 일부 ParamGetter가 제대로 작동하지 않을 수도 있다.
        if (!useForm) 
            ForceUpdate();
    }

    // SetId로 id를 설정해주면 번역테이블에서
    // 알아서 id들고와서 text에 넣는다.
    public void SetId(string id)
    {
        useId = true;
        this.id = id;
        ForceUpdate();
    }

    // 번역테이블에서 들고오는 id값 없이
    // 바로 Form을 설정.
    // Form을 설정했다는 것은 이후에 AddParam이 이어지기 때문에
    // UpdateText를 하지 않음
    public void SetForm(string form)
    {
        useForm = true;
        ts = new TransString(form);
    }

    public void AddParam(string key, Func<object> valueGetter)
    {
        if(!useForm) // SetForm을 거치지 않은 경우
        {
            // id 마저도 설정하지 않았다면 에러
            if(!useId)
            {
                Debug.LogError("문제 | 이전에 Form이 설정되지 않음");
                Debug.LogError("해결 | SetForm혹은 SetId 통해 미리 Form을 설정하거나, AddParam을 호출하지 않아야함");
                return;
            }
            ts = new TransString(translated);
            useForm = true;
        }
        ts.AddParam(key, valueGetter);
    }

    public void ForceUpdate()
    {
        // Form이 설정되어 있다면, ts를 해독해서 text에 설정
        if(useForm || useCustomForm)
        {
            text.text = ts.ToString();
            return;
        }

        string translated = this.translated;
        if (translated == null)
            return;

        // 그렇지 않다면 값을 바로 설정
        text.text = translated;
    }

}
