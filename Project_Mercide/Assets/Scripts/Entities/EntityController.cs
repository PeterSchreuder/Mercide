﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : GlobalObject
{
    [SerializeField]
    private EntityWeapon holster;
    public EntityWeapon Holster { get => holster; set => holster = value; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        holster.TeamNumber = TeamNumber;
    }

    /// <summary>
    /// Increase or Decrease health
    /// </summary>
    public virtual void HealthAdd(float _amount)
    {
        Health += _amount;

        Health = Mathf.Clamp(Health, 0, 100);

        if (_amount > 0)// Increase
        {

        }
        else// Decrease
        {
            if (Health <= 0)
            {
                Die();
            }
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Holster.Shoot();
        }
    }
}
