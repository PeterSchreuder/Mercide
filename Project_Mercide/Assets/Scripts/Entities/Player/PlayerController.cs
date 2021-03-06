﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : EntityController
{
    [SerializeField]
    private int playerIndex;
    public int PlayerIndex { get => playerIndex; set => playerIndex = value; }

    private Action<EventParam> actionListener;
    private Action<EventParam> healthListener;

    [SerializeField]
    private float invincibleTime = 1.25f;

    [SerializeField]
    private SpriteRenderer entitySprite = null;

    protected override void Awake()
    {
        base.Awake();

        actionListener = new Action<EventParam>(FireWeapon);
        healthListener = new Action<EventParam>(HealthListener);
    }

    // - Start listening
    void OnEnable()
    {
        EventManager.StartListening("InputManager:Actions", FireWeapon);
        EventManager.StartListening("EntityPlayer:AddHealth", HealthListener);
    }

    // - Stop listening
    void OnDisable()
    {
        EventManager.StopListening("InputManager:Actions", FireWeapon);
        EventManager.StopListening("EntityPlayer:AddHealth", HealthListener);
    }

    public void Damage()
    {
        HealthAdd(-10f);
    }

    void HealthListener(EventParam _data)
    {
        HealthAdd(_data.Float);
    }

    public override void HealthAdd(float _amount)
    {
        base.HealthAdd(_amount);

        if (!CheckIfDead() && _amount < 0)
        {
            SetInvincible();
        }
    }

    public override void Die()
    {
        HealthStateCurrent = EntityHealthStates.Dead;

        EventManager.TriggerEvent("Entity" + gameObject.tag + ":Died", new EventParam { Float = 100 });
    }

    public void SetInvincible()
    {
        HealthStateCurrent = EntityHealthStates.Invincible;
        StartCoroutine(InvincibleTimer(invincibleTime, 4));
    }

    IEnumerator InvincibleTimer(float _timeSec, int _blinkTimes)
    {
        for (int i = 0; i < _blinkTimes; i++)
        {
            yield return new WaitForSeconds(_timeSec / _blinkTimes);
            entitySprite.color.ChangeAlpha(0.5f);
            yield return new WaitForSeconds(_timeSec / _blinkTimes);
            entitySprite.color.ChangeAlpha(1f);
        }

        HealthStateCurrent = EntityHealthStates.Alive;
    }
}
