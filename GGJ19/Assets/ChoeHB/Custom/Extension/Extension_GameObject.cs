using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension_GameObject {
    public static void ActiveFalse(this GameObject go) => go.gameObject.SetActive(false);
    public static void ActiveTrue(this GameObject go) => go.gameObject.SetActive(true);
}
