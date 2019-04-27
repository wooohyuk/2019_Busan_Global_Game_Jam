using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public interface ITextEffect
{
    void Effect(Text text);
}

// during에 걸쳐서 scale_a에서 scale_b로
// during에 걸쳐서 alpha_a에서 alpha_b로

public class TextEffect_Multiple : ITextEffect
{
    [SerializeField] float during;
    [SerializeField] ITextEffect[] effects;
    public void Effect(Text text)
    {
        for (int i = 0; i < effects.Length; i++)
            effects[i].Effect(text);
        DOTween.Sequence().AppendInterval(during).OnComplete(text.gameObject.ActiveFalse);
    }
}

public class TextEffect_Move : ITextEffect
{
    public Vector2 dst;
    [SerializeField] float during;

    public void Effect(Text text)
    {
        Vector2 dst = this.dst + (Vector2)text.transform.position;
        text.transform.DOMove(dst, during);
    }
}


public class TextEffect_Scale : ITextEffect
{
    [SerializeField] float during;
    [SerializeField] float src = 1;
    [SerializeField] float dst = 1;

    public void Effect(Text text)
    {
        var originScale = text.transform.localScale;
        text.transform.localScale = originScale * src;
        text.transform.DOScale(originScale * dst, during)
            .OnComplete(() => text.transform.localScale = originScale);
    }
}

public class TextEffect_Alpha : ITextEffect
{
    [SerializeField] float during;

    [Range(0, 1f)]
    [SerializeField] float src = 1;

    [Range(0, 1f)]
    [SerializeField] float dst = 1;

    public void Effect(Text text)
    {
        text.Alpha(src);
        text.DOFade(dst, during);
    }

}

public class TextEffect_RandomPosition : ITextEffect
{
    [SerializeField] Vector2 range = Vector2.one;

    public void Effect(Text text)
    {
        Vector3 rand = new Vector3(
            Random.Range(-range.x, range.x),
            Random.Range(-range.y, range.y)
        );
        text.transform.position += rand;
    }
}