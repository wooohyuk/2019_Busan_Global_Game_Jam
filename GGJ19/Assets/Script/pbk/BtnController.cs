using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BtnController : MonoBehaviour {

    public bool ClickCheck;
    [SerializeField]
    Image Pause;
    private void Start()
    {
        ClickCheck = false;
    }
    public void SceneChange()
    {
        SceneManager.LoadScene("StageSelect");
    }
   
    public void ClickOn()
    {
        ClickCheck = true;
        //스테이지 선택
    }
    public void EnterStage()
    {
        SceneManager.LoadScene("InGame");
    }
    public void ChangeAble()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
        }
        else
            Time.timeScale = 1;
        Pause.gameObject.SetActive(!Pause.gameObject.active);
    }
}
