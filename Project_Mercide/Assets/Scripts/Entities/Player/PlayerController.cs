using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController
{
    [SerializeField]
    private int playerIndex;
    public int PlayerIndex { get => playerIndex; set => playerIndex = value; }

    private Action<EventParam> actionListener;

    protected override void Awake()
    {
        base.Awake();

        actionListener = new Action<EventParam>(FireWeapon);
    }

    // - Start listening
    void OnEnable()
    {
        EventManager.StartListening("InputManager:Actions", FireWeapon);
    }

    // - Stop listening
    void OnDisable()
    {
        EventManager.StopListening("InputManager:Actions", FireWeapon);
    }

    public void Damage()
    {
        HealthAdd(-10f);
    }

    public override void Die()
    {
        EventManager.TriggerEvent("Entity" + gameObject.tag + ":Died", new EventParam { Float = 100 });

        transform.Rotate(0f, 0f, 90f);
    }
}
