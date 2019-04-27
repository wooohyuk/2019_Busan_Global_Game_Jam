using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeUI : MonoBehaviour {

    [SerializeField] Transform child;

    [Button]
    public void SetSight(Sight sight)
    {
        var bounds = sight.GetBounds();
        var center = bounds.center;

        child.transform.localPosition = center;
        child.transform.localScale = new Vector3(bounds.size.x, bounds.size.y, 1);
    }

    public void Float() => gameObject.SetActive(true);
    public void Close() => gameObject.SetActive(false);
}
