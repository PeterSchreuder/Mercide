using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIStates { Idle, Alerted, AlertedDucked, Staggered, Shooting, Charging, RePositioning };

public class EnemyStateManager : MonoBehaviour
{
    private TextMesh textDebugState;

    private GlobalObject globalObject;

    private PlayerController mainTarget = null;

    private AIStates aiStateCurrent;
    public AIStates AIStateCurrent
    {
        get => aiStateCurrent;

        set
        {
            AIStatePrevious = AIStateCurrent;
            aiStateCurrent = value;

            TextUpdateDebugState();

            switch (AIStateCurrent)
            {
                case AIStates.Idle:// Doing nothing

                    break;
                case AIStates.Alerted:// In player screen

                    break;
                case AIStates.AlertedDucked:// Alerted but ducked

                    break;
                case AIStates.Staggered:

                    break;
                case AIStates.Shooting:// Just shooting

                    break;
                case AIStates.Charging:// Running at the player

                    break;
                case AIStates.RePositioning:// Moving to another (better point)

                    break;
            }
        }
    }

    private AIStates aiStatePrevious;
    public AIStates AIStatePrevious
    {
        get => aiStatePrevious;

        set
        {
            aiStatePrevious = value;
        }
    }

    private void Awake()
    {
        // Get the components
        textDebugState = GetComponentInChildren<TextMesh>();

        globalObject = GetComponent<GlobalObject>();
    }

    private void FixedUpdate()
    {
        switch (AIStateCurrent)
        {
            case AIStates.Idle:// Doing nothing



                break;
            case AIStates.Alerted:// In player screen

                break;
            case AIStates.AlertedDucked:// Alerted but ducked

                break;
            case AIStates.Staggered:

                break;
            case AIStates.Shooting:// Just shooting

                break;
            case AIStates.Charging:// Running at the player

                break;
            case AIStates.RePositioning:// Moving to another (better point)

                break;
        }
    }

    private IEnumerator WaitForAction(AIStates _state, float _timeSec)
    {
        yield return new WaitForSeconds(_timeSec);

        switch (_state)
        {
            case AIStates.Idle:// Doing nothing

                break;
            case AIStates.Alerted:// In player screen

                break;
            case AIStates.AlertedDucked:// Alerted but ducked

                break;
            case AIStates.Staggered:

                break;
            case AIStates.Shooting:// Just shooting

                break;
            case AIStates.Charging:// Running at the player

                break;
            case AIStates.RePositioning:// Moving to another (better point)

                break;
        }

        //yield return new WaitForSeconds(_timeSec);
    }

    private void TextUpdateDebugState()
    {
        if (textDebugState)
            textDebugState.text = AIStateCurrent.ToString();
        else
            Debug.LogWarning("No text object found");
    }

    /// <summary>
    /// Checks if this enemy is in the view of the target
    /// </summary>
    /// <returns></returns>
    private bool TargetCheckInView()
    {
        bool _return = false;


        return _return;
    }
}
