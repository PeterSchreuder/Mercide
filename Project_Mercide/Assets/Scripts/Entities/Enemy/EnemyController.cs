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

    protected void FireWeapon(EventParam _input)
    {
        if (_input.Bool)
            Holster.Shoot();
    }
}
