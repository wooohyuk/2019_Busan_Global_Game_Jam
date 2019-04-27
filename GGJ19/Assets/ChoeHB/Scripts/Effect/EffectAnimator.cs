using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAnimator : AdvStaticComponent<EffectAnimator> {

    [SerializeField] Effect prefab;
    LocalObjectPool<Effect> pool;

    protected override void Initialize()
    {
        pool = new LocalObjectPool<Effect>(prefab, transform, 10);
    }

    public static void Play(string id, Vector3 position, float scale = 1)
    {
        var effect = instance.pool.GetPooledObject();
        effect.Play(id, position, scale);
    }

}
