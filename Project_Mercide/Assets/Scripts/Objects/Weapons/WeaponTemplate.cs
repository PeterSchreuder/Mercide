using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons/TestWeapon")]
public class WeaponTemplate : ScriptableObject
{
    public string wpName = "WeaponTemplate";

    public float wpFireRate = 1;
    public AudioClip wpSound;

    public float wpDamage = 25;

    public string wpBulletResourceName = "Weapons/Guns/Bullet";
}
