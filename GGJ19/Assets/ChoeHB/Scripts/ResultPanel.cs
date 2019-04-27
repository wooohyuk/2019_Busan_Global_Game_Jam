using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultPanel : AdvStaticComponent<ResultPanel> {

    [SerializeField] AudioClip clearSound;
    [SerializeField] AudioClip failSound;

    [SerializeField] FloatingUI panel;

    [SerializeField] GameObject victoryImage;
    [SerializeField] GameObject loseImage;

    public void Clear()
    {
        panel.Float();
        victoryImage.gameObject.SetActive(true);
        AudioManager.PlaySound(clearSound);
    }

    private bool isFail;
    public void Fail()
    {
        if (isFail)
            return;
        isFail = true;
        loseImage.gameObject.SetActive(true);
        panel.Float();
        AudioManager.PlaySound(failSound);
    }


    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        SceneManager.LoadScene("menuSCene");
    }

}
