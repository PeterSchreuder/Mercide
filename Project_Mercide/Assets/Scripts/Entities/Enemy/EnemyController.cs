using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EntityController
{
    delegate void Hit(AIStates _state);
    Hit gotHit;

    protected override void Start()
    {
        base.Start();

        gotHit += gameObject.GetComponent<EnemyStateManager>().SetAIStateCurrent;
    }

    public override void HealthAdd(float _amount)
    {
        base.HealthAdd(_amount);

        if (_amount < 0)
            gotHit(AIStates.Staggered);
    }
}
