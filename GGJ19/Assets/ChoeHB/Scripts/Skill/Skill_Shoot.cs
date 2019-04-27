using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Shoot : Skill
{
    [SerializeField] Transform src;
    [SerializeField] Transform dst;

    [Header("Bullet")]
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] float speed;
    [SerializeField] float life;

    private LocalObjectPool<Bullet> bulletPool;

    private void Awake()
    {
        bulletPool = new LocalObjectPool<Bullet>(bulletPrefab, transform, 3, b => b.transform.SetParent(transform));
    }

    private void Reset()
    {
        src = transform;
        dst = new GameObject("dst").transform;
        dst.SetParent(transform, false);
    }

    protected override void _Active(Attack attack, Character target)
    {
        throw new System.NotImplementedException();
    }

    protected override void _Active(Attack attack, List<string> targetTags)
    {
        Vector3 dir = dst.position - src.position;
        Bullet bullet = bulletPool.GetPooledObject();
            bullet.transform.position = src.transform.position;
            bullet.Shooted(dir, speed, life, attack, targetTags);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(src.position, dst.position);
    }
}
