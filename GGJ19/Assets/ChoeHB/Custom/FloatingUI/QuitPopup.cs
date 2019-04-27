using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0114

public class QuitPopup : FloatingDialog {

    public void Float()
    {
        Float("", Application.Quit);
    }

}
