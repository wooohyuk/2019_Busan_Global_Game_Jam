using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Sight : SerializedMonoBehaviour {

    public event Action<GameObject> OnDetectIn;
    public event Action<GameObject> OnDetectOut;

    public List<string> targetTags = new List<string>();

    public void AddTargetTag(string tag) => targetTags.Add(tag);

    [SerializeField] BoxCollider2D box;

    public Bounds GetBounds()
    {
        Bounds bounds = new Bounds();
        bounds.size = box.size;
        bounds.center = box.offset;
        return bounds;
    }

    private void Reset()
    {
        box = GetComponent<BoxCollider2D>();
        box.isTrigger = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!targetTags.Contains(collision.tag))
            return;
        OnDetectIn?.Invoke(collision.gameObject);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (!targetTags.Contains(collision.tag))
            return;
        OnDetectOut?.Invoke(collision.gameObject);
    }

}
