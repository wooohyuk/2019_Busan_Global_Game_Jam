using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileAnimator : MonoBehaviour {

    [SerializeField] SpriteRenderer sr;

    [SerializeField] Sprite normal;
    [SerializeField] Sprite highlight;
    [SerializeField] Sprite warning;
    [SerializeField] float animatingTime = 0.3f;

    private void Awake() => Normal();

    public void Normal()    => sr.sprite = normal;
    public void Highlight() => sr.sprite = highlight;
    public void Warning()   => sr.sprite = warning;

    [Button]
    public void Enable() {
        if (Application.isPlaying)
            sr.DOFade(1, animatingTime);
        else
            sr.Alpha(1);
    }

    [Button]
    public void Disable() {
        if (Application.isPlaying)
            sr.DOFade(0, animatingTime);
        else
            sr.Alpha(0);
    } 

    private void OnValidate() => Normal();
}
