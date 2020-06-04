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
            Debug.LogError("No weaponTemplate found!");

        audioSource = GetComponent<AudioSource>();

        bulletPrefab = Resources.Load(weaponTemplate.wpBulletResourceName) as GameObject;
    }

    /// <summary>
    /// Shoots the weapon if canShoot == true
    /// </summary>
    public virtual void Shoot()
    {
        if (!canShoot)
            return;

        // Disable shooting
        canShoot = false;
        StartCoroutine(ShootTimer(weaponTemplate.wpFireRate));

        // Play weapon sound
        audioSource.pitch = Random.Range(0.95f, 1.10f);
        audioSource.PlayOneShot(weaponTemplate.wpSound);

        GameObject _bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        _bullet.GetComponent<Bullet>().TeamNumber = TeamNumber;
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
}
