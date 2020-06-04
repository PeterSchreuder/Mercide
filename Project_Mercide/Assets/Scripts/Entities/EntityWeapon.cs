using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityWeapon : GlobalObject
{
    protected GameObject bulletPrefab;
    protected AudioSource audioSource;

    [SerializeField]
    protected float fireRate = 1;



    protected bool canShoot = true;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        audioSource = GetComponent<AudioSource>();

        bulletPrefab = Resources.Load("Bullet") as GameObject;
    }


    public virtual void Shoot()
    {
        if (!canShoot)
            return;

        canShoot = false;
        StartCoroutine(ShootTimer(fireRate));

        audioSource.pitch = Random.Range(0.95f, 1.10f);
        audioSource.PlayOneShot(audioSource.clip);

        GameObject _bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        _bullet.GetComponent<Bullet>().TeamNumber = TeamNumber;
    }

    public IEnumerator ShootTimer(float _fireRate)
    {

        yield return new WaitForSeconds(_fireRate);

        canShoot = true;
    }
}
