using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class CharacterAnimator : SerializedMonoBehaviour {

    [SerializeField] AudioClip attackSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip hitedSound;


    [SerializeField] protected Animator animator;
    [SerializeField] bool useWhiteEffect = true;
    [SerializeField] SpriteRenderer shadow;

    private const float deaingSize = 1.3f;
    private const float deaingTime = 0.5f;
    private const float whitingTime = 0.3f;
    
    private static Material _white;
    private static Material white
    {
        get
        {
            if (_white == null)
                _white = Resources.Load<Material>("White");
            return _white;
        }
    }

    private SpriteRenderer _sr;
    protected SpriteRenderer sr
    {
        get
        {
            if (_sr == null)
                _sr = GetComponent<SpriteRenderer>();
            return _sr;
        }
    }

    private Material defaultMaterial;


    protected virtual void Awake()
    {
        defaultMaterial = sr.material;
        
    }

    private void OnEnable()
    {
        sr.material = defaultMaterial;

        transform.localScale = Vector3.one;

        sr.Alpha(1);
        shadow.Alpha(0.5f);
    }

    private void Update()
    {
        sr.sortingOrder = (int)(transform.position.y * -1000);
    }

    private void Reset()
    {
        animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
        if (attackSound != null)
            AudioManager.PlaySound(attackSound);
    }

    public void Dead()
    {
        sr.material = defaultMaterial;
        animator.SetTrigger("Dead");

        transform.DOScale(deaingSize, deaingTime);
        sr.DOFade(0, deaingTime);

        shadow.DOFade(0, deaingTime);
        if (deathSound != null)
            AudioManager.PlaySound(deathSound);
    }

    public void Hited()
    {
        if (deathSound != null)
            AudioManager.PlaySound(hitedSound);

        if (!useWhiteEffect)
            return;
        sr.material = white;
        Invoke(nameof(_Hited), whitingTime);
    }

    private void _Hited()
    {
        sr.material = defaultMaterial;
    }

}
