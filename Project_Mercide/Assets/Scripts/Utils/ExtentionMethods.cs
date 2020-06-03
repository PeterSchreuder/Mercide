using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class ExtentionMethods
{
    public static Vector3 AddX(this Vector3 value, float add)
    {
        return new Vector3(value.x + add, value.y, value.z);
    }

    public static Vector3 AddY(this Vector3 value, float add)
    {
        return new Vector3(value.x, value.y + add, value.z);
    }

    public static Vector3 AddZ(this Vector3 value, float add)
    {
        return new Vector3(value.x, value.y, value.z + add);
    }

    public static Vector3 Round(this Vector3 _vector3)
    {
        return new Vector3(Mathf.Round(_vector3.x), Mathf.Round(_vector3.y), Mathf.Round(_vector3.z));
    }

    public static Vector3 Multiply(this Vector3 _v1, Vector3 _v2)
    {
        return new Vector3(_v1.x * _v2.x, _v1.y * _v2.y, _v1.z * _v2.z);
    }

    public static float Round(this float _float)
    {
        return Mathf.Round(_float);
    }
}

