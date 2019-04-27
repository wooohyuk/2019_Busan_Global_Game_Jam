using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectUI : MonoBehaviour {

    [SerializeField] GameObject titlePanel;
    [SerializeField] Animator animator;

    public void StartStage(int stage)
    {
        Stage.StartStage(stage);
        gameObject.SetActive(false);
    }

    public void PressStart()
    {
        animator.SetTrigger("Chapter");
        //titlePanel.gameObject.SetActive(false);
    }

    public void PressHelp()
    {

    }

}
