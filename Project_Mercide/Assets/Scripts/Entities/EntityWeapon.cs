using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityWeapon : GlobalObject
{
    protected GameObject bulletPrefab;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        bulletPrefab = Resources.Load("Bullet") as GameObject;
    }


    public virtual void Shoot()
    {
        GameObject _bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        _bullet.GetComponent<Bullet>().TeamNumber = TeamNumber;
        print("Pew");
    }
}
