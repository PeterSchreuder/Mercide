using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityWeapon : GlobalObject
{
    public WeaponTemplate weaponTemplate;

    protected GameObject bulletPrefab;
    protected AudioSource audioSource;

    protected bool canShoot = true;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        if (!weaponTemplate)
        {
            Debug.LogError("No weaponTemplate found!");
        }
        else
        {
            weaponTemplate.WeaponInitialize();
        }

        audioSource = GetComponent<AudioSource>();

        bulletPrefab = Resources.Load(weaponTemplate.wpBulletResourceName) as GameObject;
    }

    /// <summary>
    /// Shoots the weapon if canShoot == true
    /// </summary>
    public virtual void Shoot()
    {
        if (!canShoot && CheckIfOwnerIsAlive())
            return;

        if (weaponTemplate.wpAmmoCurrent == 0 && weaponTemplate.wpAmmoBackup == 0)
        {
            print("Out of ammo");
            return;
        }
        else if (weaponTemplate.wpAmmoCurrent == 0)
        {
            print("reload");
            return;
        }

        // Disable shooting
        canShoot = false;
        //weaponTemplate.wpAmmoCurrent--;
        StartCoroutine(ShootTimer(weaponTemplate.wpFireRate));

        // Play weapon sound
        audioSource.pitch = Random.Range(0.95f, 1.10f);
        audioSource.PlayOneShot(weaponTemplate.wpSound);

        // Create the bullet
        GameObject _bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Bullet _bulletScript = _bullet.GetComponent<Bullet>();

        _bulletScript.bulletTemplate.bltDamage = weaponTemplate.wpDamage;
        _bulletScript.bulletTemplate.bltOwner = weaponTemplate.wpOwner;
        _bulletScript.TeamNumber = TeamNumber;
    }

    /// <summary>
    /// Enables shooting after X time
    /// </summary>
    /// <param name="_fireRate"></param>
    /// <returns></returns>
    public IEnumerator ShootTimer(float _fireRate)
    {
        yield return new WaitForSeconds(_fireRate);

        canShoot = true;
    }

    bool CheckIfOwnerIsAlive()
    {
        bool _return = false;

        if (weaponTemplate.wpOwner && weaponTemplate.wpOwner.HealthStateCurrent != EntityHealthStates.Dead)
            _return = true;

        return _return;
    }
}
