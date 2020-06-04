using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons/TestWeapon")]
public class WeaponTemplate : ScriptableObject
{
    [Header("Data")]
    public string wpName = "WeaponTemplate";
    public float wpFireRate = 0.1f;
    public float wpDamage = 25;

    [Header("Ammo")]
    public int wpMagazineSize = 30;
    public int wpAmmoCurrent = 0;
    public int wpAmmoBackup = 90;
    [HideInInspector]
    public int wpAmmoBackupMax = 0;

    [Header("Resources")]
    public AudioClip wpSound;
    public string wpBulletResourceName = "Weapons/Guns/Bullet";

    /// <summary>
    /// Initialize the weapon
    /// </summary>
    public void WeaponInitialize()
    {
        wpAmmoBackupMax = wpAmmoBackup;
    }
}
