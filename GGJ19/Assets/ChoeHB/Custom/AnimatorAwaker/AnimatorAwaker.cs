using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorAwaker : MonoBehaviour {

    private Animator _animator;
    private Animator animator
    {
        get {
            if (_animator == null)
                _animator = GetComponent<Animator>();
            return _animator;
        }
    }

    [Header("Use Bool")]
    [SerializeField] bool useBool;
    [ShowIf(nameof(useBool))][SerializeField] string boolName;
    [ShowIf(nameof(useBool))][SerializeField] bool boolValue;

    [Header("Use Trigger")]
    [SerializeField] bool useTrigger;
    [ShowIf(nameof(useTrigger))]
    [SerializeField] string triggerName;

    [Header("Use Float")]
    [SerializeField] bool useFloat;
    [ShowIf(nameof(useFloat))] [SerializeField] string floatName;
    [ShowIf(nameof(useFloat))] [SerializeField] float floatValue;

    private void Awake() => StartCoroutine(Awaking());

    private IEnumerator Awaking()
    {
        yield return new WaitUntil(() => animator.isActiveAndEnabled);
        if (useBool)
            animator.SetBool(boolName, boolValue);
        if (useTrigger)
            animator.SetTrigger(triggerName);
        if (useFloat)
            animator.SetFloat(floatName, floatValue);
    }
    

}
