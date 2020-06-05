using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GlobalObject
{
    public BulletTemplate bulletTemplate;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletTemplate.bltSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EntityController _otherEntityController = other.gameObject.GetComponent<EntityController>();
        bool _destroy = false;

        if (_otherEntityController)
        {
            if (_otherEntityController.TeamNumber != TeamNumber)
            {
                _destroy = true;

                _otherEntityController.HealthAdd(-bulletTemplate.bltDamage);
            }
        }
        else
        {
            _destroy = true;
        }
        
        if (_destroy)
        {
            Die();
        }
    }


}
