using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluRotate : MonoBehaviour {

    [SerializeField] float angle;

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, angle * Time.deltaTime));
    }
}
