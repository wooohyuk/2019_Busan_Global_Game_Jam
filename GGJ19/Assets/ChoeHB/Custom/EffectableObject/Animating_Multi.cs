using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

public class Animating_Multi : MonoBehaviour {

    [EnumToggleButtons]
    public enum Type {
        Trigger, Bool, Float
    }

    [SerializeField] Animator[] animators;
    [SerializeField] float interval;
    
    [SerializeField] Type type;

    [SerializeField] string param;

    [ShowIf(nameof(type), Type.Bool)]
    [SerializeField] bool flag;

    [ShowIf(nameof(type), Type.Float)]
    [SerializeField] float f;

    [Button]
    private void Reset()
    {
        animators = GetComponentsInChildren<Animator>();
    }

    public void Animate() => StartCoroutine(Animating());

    private IEnumerator Animating()
    {
        foreach (var a in animators)
        {
            if (type == Type.Bool)
                a.SetBool(param, flag);

            if (type == Type.Trigger)
                a.SetTrigger(param);

            if (type == Type.Float)
                a.SetFloat(param, f);

            yield return new WaitForSeconds(interval);
        }
    }

    
    
}
