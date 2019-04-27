using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingUI : MonoBehaviour
{
    [Header("Floating UI")]
    [SerializeField] Animator animator;
    [SerializeField] AudioClip floatingSound;
    [SerializeField] AudioClip closingSound;
    [SerializeField] protected bool useBackButtonClose = true;

    protected bool isFloated { get; private set; }
    protected bool isInitialized;

    private void Start()
    {
        if(!isInitialized)
            Initialize();
    }

    // Floating UI는 인위적으로 false 상태의 GameObject를 true로 바꿔주는 경우도 있기 때문에
    // Start나 Awake보다 Float가 먼저 실행될 가능성이 있음.
    // 그렇기 때문에 따로 구현한 Initialize를 상속하여 초기화를 구현
    protected virtual void Initialize()
    {
        Debug.Assert(!isInitialized);
        animator = GetComponent<Animator>();
        isInitialized = true;
    }

    [Button]
    public virtual void Float()
    {
        if (!isInitialized)
            Initialize();

        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        if (animator)
            animator.SetBool("Float", true);

        if (floatingSound)
            AudioManager.PlaySound(floatingSound);

        if (useBackButtonClose)
            InputManager.AddBackButtonEvent(Close);

        isFloated = true;
    }

    [Button]
    public void Close()
    {
        if (useBackButtonClose)
            InputManager.RemoveBackButtonEvent(Close);

        if (closingSound)
            AudioManager.PlaySound(closingSound);

        if (animator)
            animator.SetBool("Float", false);
        else
            gameObject.SetActive(false);

        isFloated = false;
    }

    private void Reset()
    {
        animator = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        if (useBackButtonClose)
            InputManager.RemoveBackButtonEvent(Close);
    }


}
