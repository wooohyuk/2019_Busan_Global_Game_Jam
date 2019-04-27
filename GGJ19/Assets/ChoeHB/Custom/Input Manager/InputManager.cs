using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*---------------------
 * Ex) InputManager.OnPressReturn += Close;
 *---------------------*/

public class InputManager : AdvSingletonComponent<InputManager>
{
    public static event Action OnPressReturn;   // Enter
    public static event Action OnPressSpace;
    public static event Action OnPressCtrl;
    public static event Action OnPressTab;

    public static event Action OnTouch;

    public static event Action<int> OnPressNum;

    public static event Action OnPressEscape;   // Esc
    private Stack<Action> backButtonEvents = new Stack<Action>();

    #region Mouse 

    public static Vector2 mousePosition => Camera.main.ScreenToWorldPoint((Vector2)(Input.mousePosition));

    #endregion

    public static void AddBackButtonEvent(Action act)
    {
        instance.backButtonEvents.Push(act);
    }

    public static void RemoveBackButtonEvent(Action act)
    {
        if (instance.backButtonEvents.Count == 0)
            return;
        
        if (instance.backButtonEvents.Peek() == act)
            instance.backButtonEvents.Pop();
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
            if (OnTouch != null)
                OnTouch();

        if (Input.GetKeyDown(KeyCode.Return))
            if (OnPressReturn != null)
                OnPressReturn();

        if (Input.GetKeyDown(KeyCode.Space))
            if (OnPressSpace != null)
                OnPressSpace();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (backButtonEvents.Count != 0)
                backButtonEvents.Pop()();

            if (OnPressEscape != null)
                OnPressEscape();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
            if (OnPressCtrl != null)
                OnPressCtrl();

        if (Input.GetKeyDown(KeyCode.Tab))
            if (OnPressTab != null)
                OnPressTab();

        CheckNumPressDown();
    }

    void CheckNumPressDown()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            if (OnPressNum != null)
                OnPressNum(0);
        if (Input.GetKeyDown(KeyCode.Alpha1))
            if (OnPressNum != null)
                OnPressNum(1);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            if (OnPressNum != null)
                OnPressNum(2);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            if (OnPressNum != null)
                OnPressNum(3);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            if (OnPressNum != null)
                OnPressNum(4);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            if (OnPressNum != null)
                OnPressNum(5);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            if (OnPressNum != null)
                OnPressNum(6);
        if (Input.GetKeyDown(KeyCode.Alpha7))
            if (OnPressNum != null)
                OnPressNum(7);
        if (Input.GetKeyDown(KeyCode.Alpha8))
            if (OnPressNum != null)
                OnPressNum(8);
        if (Input.GetKeyDown(KeyCode.Alpha9))
            if (OnPressNum != null)
                OnPressNum(9);

    }

    
}
