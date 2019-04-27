using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Extension_Color
{

    public static Color HSBLerp(this Color src, Color dst, float t)
    {
        float srcH, srcS, srcV;
        float dstH, dstS, dstV;

        Color.RGBToHSV(src, out srcH, out srcS, out srcV);
        Color.RGBToHSV(dst, out dstH, out dstS, out dstV);

        float h, s, v;

        h = Mathf.Lerp(srcH, dstH, t);
        s = Mathf.Lerp(srcS, dstS, t);
        v = Mathf.Lerp(srcV, dstV, t);

        return Color.HSVToRGB(h, s, v);
    }

    public static void Alpha(this Graphic graphic, float a)
    {
        var newColor = graphic.color.Alpha(a);
        graphic.color = newColor;
    }

    public static Color Alpha(this Color color, float a)
    {
        color.a = a;
        return color;
    }

    public static string Fill(this string text, string color)
    {
        return string.Format("<color={0}>{1}</color>", color, text);
    }

    public static string Fill(this string text, Color color)
    {
        return Fill(text, "#" + ColorUtility.ToHtmlStringRGBA(color));
    }

}