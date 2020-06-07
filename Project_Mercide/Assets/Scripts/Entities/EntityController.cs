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
            EventManager.TriggerEvent("Entity" + gameObject.tag + ":GotHealth");
        }
        else// Decrease
        {
            EventManager.TriggerEvent("Entity" + gameObject.tag + ":GotDamage");// If Player: red screen flash, if Enemy: Hitmarker sound

            if (Health <= 0)
            {
                Die();
            }
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }
}



