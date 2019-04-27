using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension_Coroutine {

    public static IEnumerator After(this Action action, float after)
    {
        yield return new WaitForSeconds(after);
        action();
    }

}
