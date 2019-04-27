using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HorizontalLayoutGroup))]
[RequireComponent(typeof(CanvasGroup))]
public class ToggleGuageBar : MonoBehaviour {

    [SerializeField] Toggle prefab;

    private List<Toggle> toggles;
    private LocalObjectPool<Toggle> pool;

    [ShowInInspector,ReadOnly] public int value        { get; private set; }
    [ShowInInspector,ReadOnly] public int maxValue     { get; private set; }

    private HorizontalLayoutGroup _hlg;
    private HorizontalLayoutGroup hlg {
        get {
            if (_hlg == null)
                _hlg = GetComponent<HorizontalLayoutGroup>();
            return _hlg;
        }
    }

    private void Reset()
    {
        prefab = GetComponentInChildren<Toggle>();
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    private bool isInitialized;
    public void Initialize(int maxValue) => Initialize(maxValue, maxValue);
    public void Initialize(int maxValue, int value) {

        this.maxValue   = int.MinValue;
        this.value      = int.MinValue;

        if (isInitialized)
        {
            SetMaxValue(maxValue);
            SetValue(value);
            return;
        }

        isInitialized = true;
        toggles = new List<Toggle>();

        // Pool
        prefab.gameObject.SetActive(false);
        pool = new LocalObjectPool<Toggle>(prefab, transform, maxValue, toggle => 
            toggle.transform.SetParent(transform, false));

        SetMaxValue(maxValue);
        SetValue(value);

        Canvas.ForceUpdateCanvases();
        gameObject.SetActive(false);
        gameObject.SetActive(true);
        hlg.enabled = false;
        hlg.enabled = true;
    }
    
    [Button]
    public void SetMaxValue(int maxValue)
    {
        if (this.maxValue == maxValue)
            return;

        this.maxValue = maxValue;
        if (toggles.Count < maxValue)
        {
            int dif = maxValue - toggles.Count;
            for (int i = 0; i < dif; i++)
                toggles.Add(pool.GetPooledObject());
        }

        if (maxValue < toggles.Count)
        {
            for (int i = 0; i < toggles.Count - maxValue; i++)
            {
                var toggle = toggles[toggles.Count - 1];
                toggles.Remove(toggle);
                toggle.gameObject.SetActive(false);
            }
        }
    }

    [Button]
    public void SetValue(int value)
    {
        if (this.value == value)
            return;
        this.value = value;
        for (int i = 0; i < toggles.Count; i++)
            toggles[i].isOn = i < value;
    }
}
