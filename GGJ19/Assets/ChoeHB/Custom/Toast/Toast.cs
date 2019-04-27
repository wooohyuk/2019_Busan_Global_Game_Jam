using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toast : StaticComponent<Toast> {

    [SerializeField] Text text;
    [SerializeField] float during;
    [SerializeField] float fading;

    public static void Float(string text)
    {
        instance.Float_(text);
    }

    private void Float_(string text)
    {
        gameObject.SetActive(true);
        this.text.text = text;
        var seq = DOTween.Sequence();
            seq.Append(this.text.DOFade(1, 0));
            seq.AppendInterval(during);
            seq.Append(this.text.DOFade(0, fading));
            seq.OnComplete(() => gameObject.SetActive(false));
    }
}
