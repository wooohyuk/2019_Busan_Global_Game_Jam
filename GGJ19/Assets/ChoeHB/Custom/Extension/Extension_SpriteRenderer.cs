using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension_SpriteRenderer {

    public static void Alpha(this SpriteRenderer sr, float a)
    {
        var color = sr.color;
        color.a = a;
        sr.color = color;
    }

}
