using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(TextNamer))]
public class TextNamer : MonoBehaviour {
#if UNITY_EDITOR
    private Text text;
    private void Awake() => text = GetComponent<Text>();
    private void Update() => name = $"text_{text.text}";
#endif
}
