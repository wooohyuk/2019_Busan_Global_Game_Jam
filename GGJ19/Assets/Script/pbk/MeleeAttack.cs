using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour {

    private int MeleeAtk;
    private Tower S_Tower;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetAtk(int argAtk)
    {
        MeleeAtk = argAtk;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Towr")
        {
            S_Tower = collision.gameObject.GetComponent<Tower>();
            //S_Tower.Hp-=MeleeAtk;
            Destroy(gameObject);
        }
    }
}
