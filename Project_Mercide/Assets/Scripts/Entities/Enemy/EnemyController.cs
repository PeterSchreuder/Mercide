using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EntityController
{
    [SerializeField]
    private int playerIndex;
    public int PlayerIndex { get => playerIndex; set => playerIndex = value; }

    //private Action<EventParam> actionListener;

    protected override void Awake()
    {
        base.Awake();

        //actionListener = new Action<EventParam>(FireWeapon);
    }

    // - Start listening
    void OnEnable()
    {
        //EventManager.StartListening("InputManager:Actions", FireWeapon);
    }

    // - Stop listening
    void OnDisable()
    {
        //EventManager.StopListening("InputManager:Actions", FireWeapon);
    }

    /// <summary>
    /// Increase or Decrease health
    /// </summary>
    public override void HealthAdd(float _amount)
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
            //GetComponent<EnemyStateManager>().AIStateCurrent = AIStates.Staggered;

            if (Health <= 0)
            {
                Die();
            }
        }
    }

    protected void FireWeapon(EventParam _input)
    {
        if (_input.Bool)
            Holster.Shoot();
    }
}
