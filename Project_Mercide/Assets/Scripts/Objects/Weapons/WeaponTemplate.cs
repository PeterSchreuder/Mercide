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
    public float wpReloadSpeed = 1;
    [HideInInspector]
    public GlobalObject wpOwner = null;

    [Header("Gameplay Related")]
    public float wpRecoil = 1.5f;
    public float wpRecoilRougness = 4f;
    public float wpRecoilFadeInTime = 0.1f;
    public float wpRecoilFadeOutTime = 0.75f;

    [Header("Ammo")]
    public int wpMagazineSize = 30;
    public int wpAmmoBackup = 90;
    [HideInInspector]
    public int wpAmmoCurrent = 0, wpAmmoBackupMax = 0;

    [Header("Resources")]
    public AudioClip wpSound;
    public string wpBulletResourceName = "Weapons/Guns/Bullet";

    /// <summary>
    /// Initialize the weapon
    /// </summary>
    public void WeaponInitialize()
    {
        wpAmmoCurrent = wpMagazineSize;
        wpAmmoBackup -= wpAmmoCurrent;
        wpAmmoBackupMax = wpAmmoBackup;
    }
}
