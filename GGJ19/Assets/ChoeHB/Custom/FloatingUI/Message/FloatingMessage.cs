using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingMessage : FloatingUI {
    
    [Header("Floating Message")]
    [SerializeField] Text contextText;

    private Action OnAccept;
    
    public void Float(Action OnAccept)
    {
        this.OnAccept = OnAccept;
        Float();
    }

    public void Float(string message, Action OnAccept = null)
    {
        
        contextText.text = message;
        this.OnAccept = OnAccept;
        Float();
    }

    public void Accept()
    {
        if (OnAccept != null)
            OnAccept();
        Close();
    }

}
