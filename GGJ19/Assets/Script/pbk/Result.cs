using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour {
    [SerializeField]
    Image ResultImage;
    [SerializeField]
    Text MoneyText;
    [SerializeField]
    Text ResultText;
    [SerializeField]
    Image ResultPanel;
    bool check = true;
    [SerializeField]
    Sprite[] ResultImagArr=new Sprite[5];
    // Use this for initialization
    void Start()
    {
        ResultText.fontSize = 100;
        ResultText.color = Color.black;
        MoneyText.fontSize = 100;
        MoneyText.color = Color.black;
        MoneyText.text = "획득 금화"; //+금화 변수.toString();

        /*if(무한모드일때)
        {
            ResultPanel.gameobject.setActive(true);
        }*/
        //ResultImage = 
        /*if (check)//이겼을때
        {
            
            ResultText.text = "수호 성공";
        }
        else
            ResultText.text = "수호 실패";*/
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
}
