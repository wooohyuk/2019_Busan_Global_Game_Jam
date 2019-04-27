using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {
    [SerializeField]
    private int speed;
    Transform tr;
	// Use this for initialization
	void Start () {
        tr = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        tr.Translate(new Vector2(-speed * Time.deltaTime,0));
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Tower")
        {
            //타워 체력감소
            Destroy(gameObject);
        }
    }
}
