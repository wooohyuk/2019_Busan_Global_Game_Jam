using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPool : SerializedMonoBehaviour, Initializable {

    [SerializeField] Transform textHolder;
    [SerializeField] Text textPrefab;

    [SerializeField] int preSpawnCount = 5;
    [SerializeField] ITextEffect effect;

    private LocalObjectPool<Text> pool;

    private bool isInitialized;
    public void Initialize()
    {
        if (isInitialized)
            return;
        isInitialized = true;
        pool = new LocalObjectPool<Text>(textPrefab, textHolder, preSpawnCount);
    }

    [Button]
    private void Float(string context) => Float(context, transform.position);
    public void Float(string context, Vector3 position)
    {
        Initialize();
        Text text = pool.GetPooledObject();
        text.gameObject.SetActive(true);
        text.text = context;
        text.transform.position = position;
        effect?.Effect(text);
    }

}
