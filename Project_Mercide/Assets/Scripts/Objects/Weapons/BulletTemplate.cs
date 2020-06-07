using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Bullet", menuName = "Weapons/Bullet")]
public class BulletTemplate : ScriptableObject
{
    [Header("Data")]
    public float bltSpeed = 40f;

    [HideInInspector]
    public float bltDamage = 0f;

}
