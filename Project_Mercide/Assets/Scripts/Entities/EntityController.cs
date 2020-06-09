using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EntityController : GlobalObject
{
    [SerializeField]
    private EntityWeapon holster;
    public EntityWeapon Holster { get => holster; set => holster = value; }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        Holster.TeamNumber = TeamNumber;
        Holster.weaponTemplate.wpOwner = this;// Set the weapon owner to this script

        HealthAdd(100f);
    }

    /// <summary>
    /// Increase or Decrease health
    /// </summary>
    public virtual void HealthAdd(float _amount)
    {
        Health += _amount;

        Health = Mathf.Clamp(Health, 0, 100);

        EventParam _newHealthAmount = new EventParam { Float = Health };

        if (_amount > 0)// Increase
        {
            EventManager.TriggerEvent("Entity" + gameObject.tag + ":GotHealth", _newHealthAmount);
        }
        else// Decrease
        {
            EventManager.TriggerEvent("Entity" + gameObject.tag + ":GotDamage", _newHealthAmount);// If Player: red screen flash, if Enemy: Hitmarker sound

            if (Health <= 0)
            {
                Die();
            }
        }
    }

    protected virtual void FireWeapon(EventParam _input)
    {
        if (_input.Bool)
            Holster.Shoot();
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }
}



