using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Extension_Transform {
    
    public static void Rotation(this Transform transform, float angle)
    {
        transform.localRotation = Quaternion.Euler(0, transform.localRotation.y, angle);
    }

    public static void Rotation(this Transform transform, Vector3 dir)
    {
        transform.localRotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x));
    }

    public static void SetPosX(this Transform transform, float x)
    {
        transform.position = new Vector3(
            x,
            transform.position.y,
            transform.position.z
        );
    }

    public static void SetPosY(this Transform transform, float y)
    {
        transform.position = new Vector3(
            transform.position.x,
            y,
            transform.position.z
        );
    }

    public static void SetPosZ(this Transform transform, float z)
    {
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            z
        );
    }

    public static void SetLocalPosX(this Transform transform, float x)
    {
        transform.localPosition = new Vector3(
            x,
            transform.localPosition.y,
            transform.localPosition.z
        );
    }

    public static void SetLocalPosY(this Transform transform, float y)
    {
        transform.localPosition = new Vector3(
            transform.localPosition.x,
            y,
            transform.localPosition.z
        );
    }

    public static void SetLocalPosZ(this Transform transform, float z)
    {
        transform.localPosition = new Vector3(
            transform.localPosition.x,
            transform.localPosition.y,
            z
        );
    }

    public static void FacingRight(this Transform transform)
    {
        Vector3 angle = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(angle.x, 0, angle.z);
    }

    public static void FacingLeft(this Transform transform)
    {
        Vector3 angle = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(angle.x, 180, angle.z);
    }

}
