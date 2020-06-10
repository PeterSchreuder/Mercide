using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public static class ExtentionMethods
{

    #region Vector3

    //- Change
    public static Vector3 ChangeX(this Vector3 value, float value2)
    {
        return new Vector3(value2, value.y, value.z);
    }

    public static Vector3 ChangeY(this Vector3 value, float value2)
    {
        return new Vector3(value.x, value2, value.z);
    }

    public static Vector3 ChangeZ(this Vector3 value, float value2)
    {
        return new Vector3(value.x, value.y, value2);
    }

    //- Add
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

    #endregion

    #region Quaternion

    public static Quaternion AddX(this Quaternion value, float add)
    {
        return Quaternion.Euler(value.x + add, value.y, value.z);
    }

    public static Quaternion AddY(this Quaternion value, float add)
    {
        return Quaternion.Euler(value.x + add, value.y + add, value.z);
    }

    public static Quaternion AddZ(this Quaternion value, float add)
    {
        return Quaternion.Euler(value.x, value.y, value.z + add);
    }

    #endregion

    public static float Round(this float _float)
    {
        return Mathf.Round(_float);
    }

    public static Button OnDown(this Button _button)
    {
        Button _return = _button.OnDown();

        return _return;
    }

    /// <summary>
    /// Changes the Alpha of a Color
    /// </summary>
    /// <param name="_color"></param>
    /// <param name="_alpha">0 to 1</param>
    /// <returns></returns>
    public static Color ChangeAlpha(this Color _color, float _alpha)
    {
        return new Color(_color.r, _color.g, _color.b, _alpha);
    }

    /// <summary>
    /// Extend from bool
    /// </summary>
    /// <param name="_value"></param>
    /// <returns>false = 0f, true = 1f</returns>
    public static float ToFloat(this bool _value)
    {
        float _return = -1f;

        if (_value)
            _return = 1f;

        return _return;
    }
}

