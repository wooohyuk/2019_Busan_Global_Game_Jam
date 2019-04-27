using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {

    [SerializeField]
    Button[] StageBtn = new Button[4];
    [SerializeField]
    Image SelectImage;
    [SerializeField]
    Image StageEx;
    [SerializeField]
    public Sprite[] StageImage = new Sprite[5];
    [SerializeField]
    Sprite[] StageExImage = new Sprite[5];
    // Use this for initialization
    void Start()
    {
        
        
    }

	
	// Update is called once per frame
	void Update ()
    {
	    if(StageBtn[0].GetComponent<BtnController>().ClickCheck)
        {
            StageBtn[1].GetComponent<BtnController>().ClickCheck=false;
            StageBtn[2].GetComponent<BtnController>().ClickCheck=false;
            StageBtn[3].GetComponent<BtnController>().ClickCheck = false;
            SelectImage.sprite = StageImage[1];
            StageEx.sprite = StageExImage[1];
            StageBtn[0].GetComponent<BtnController>().ClickCheck = false;
        }
        if (StageBtn[1].GetComponent<BtnController>().ClickCheck)
        {
            StageBtn[0].GetComponent<BtnController>().ClickCheck = false;
            StageBtn[2].GetComponent<BtnController>().ClickCheck = false;
            StageBtn[3].GetComponent<BtnController>().ClickCheck = false;
            SelectImage.sprite = StageImage[2];
            StageEx.sprite = StageExImage[2];
            StageBtn[1].GetComponent<BtnController>().ClickCheck = false;
        }
        if (StageBtn[2].GetComponent<BtnController>().ClickCheck)
        {
            StageBtn[1].GetComponent<BtnController>().ClickCheck = false;
            StageBtn[0].GetComponent<BtnController>().ClickCheck = false;
            StageBtn[3].GetComponent<BtnController>().ClickCheck = false;
            SelectImage.sprite = StageImage[3];
            StageEx.sprite = StageExImage[3];
            StageBtn[2].GetComponent<BtnController>().ClickCheck = false;
        }
        if (StageBtn[3].GetComponent<BtnController>().ClickCheck)
        {
            StageBtn[1].GetComponent<BtnController>().ClickCheck = false;
            StageBtn[2].GetComponent<BtnController>().ClickCheck = false;
            StageBtn[0].GetComponent<BtnController>().ClickCheck = false;
            SelectImage.sprite = StageImage[4];
            StageEx.sprite = StageExImage[4];
            StageBtn[3].GetComponent<BtnController>().ClickCheck = false;
        }

    }
}
