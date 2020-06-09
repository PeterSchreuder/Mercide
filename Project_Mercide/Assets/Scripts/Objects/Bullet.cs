using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GlobalObject
{
    public BulletTemplate bulletTemplate;

    private Vector2 startPosition;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        transform.Translate((transform.right * bulletTemplate.bltSpeed) * deltaTime, Space.World);

        if (transform.position.x - startPosition.x >= bulletTemplate.bltRange)
            Die();
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
        else if (!other.CompareTag("Trigger"))
        {
            _destroy = true;
        }
        
        if (_destroy)
        {
            Die();
        }
    }


}
