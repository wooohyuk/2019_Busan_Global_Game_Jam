using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Bullet : MonoBehaviour {

    private Vector3 dir;
    private float speed;
    private float life;
    private Attack attack;
    private List<string> targetTags;

    private float atShooted;

    private void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public void Shooted(Vector3 dir, float speed, float life, Attack attack, List<string> targetTags)
    {
        atShooted       = Time.time;
        this.dir        = dir.normalized;
        this.speed      = speed;
        this.life       = life;
        this.attack     = attack;
        this.targetTags = targetTags;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (atShooted + life < Time.time)
            gameObject.SetActive(false);
        transform.position += dir.normalized * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (targetTags == null)
            return;

        if (!targetTags.Contains(collision.tag))
            return;

        Character target = collision.GetComponent<Character>();
        target.Hited(attack);
        gameObject.SetActive(false);
    }
}
