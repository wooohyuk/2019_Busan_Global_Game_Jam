using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingDialog : FloatingUI {

    [Header("Floating Dialog")]
    [SerializeField] Text titleText;
    [SerializeField] Text contextText;

    private Action OnAccept;
    private Action OnReject;

    public virtual void Float(string title, string message, Action OnAccept, Action OnReject = null)
    {
        titleText.text = title;
        Float(message, OnAccept, OnReject);
    }


    public virtual void Float(string message, Action OnAccept, Action OnReject = null)
    {
        contextText.text = message;
        Float(OnAccept, OnReject);
    }

    public virtual void Float(Action OnAccept, Action OnReject = null)
    {

        this.OnAccept = OnAccept;
        this.OnReject = OnReject;

        Float();

        if (useBackButtonClose)
        {
            InputManager.RemoveBackButtonEvent(Close);
            InputManager.AddBackButtonEvent(Reject);
        }
    }

    public void Accept()
    {
        if (OnAccept != null)
            OnAccept();
        Close();
    }

    public void Reject()
    {
        if (OnReject != null)
            OnReject();
        InputManager.RemoveBackButtonEvent(Reject);
        Close();
    }

}
