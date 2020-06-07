using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIStates { Idle, Alerted, AlertedDucked, Staggered, Shooting, Charging, RePositioning };

public enum TargetVecticalPosition { Same, Above, Under };// If the target is above the object, bellow or at the same level

public class EnemyStateManager : MonoBehaviour
{
    public EnemyTemplate enemyTemplate; 

    private TextMesh textDebugState;

    private GlobalObject globalObject;
    private EnemyMovement enemyMovement;
    private EnemyController enemyController;
    private EnemyWeapon enemyWeapon;

    private List<PlayerController> targets = new List<PlayerController>();
    public List<PlayerController> Targets { get => targets; set => targets = value; }

    private PlayerController mainTarget = null;
    private bool crIsRunning = false;// if the Coroutine is running

    // Data
    private float staggerTime;
    //private float actionTime = 5f;

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
        enemyMovement = GetComponent<EnemyMovement>();
        enemyController = GetComponent<EnemyController>();
        enemyWeapon = GetComponent<EnemyWeapon>();

        // Update the target list
        TargetUpdateList();

        // Set the AI state as last
        AIStateCurrent = AIStates.Idle;
    }

    private void FixedUpdate()
    {
        switch (AIStateCurrent)
        {
            case AIStates.Idle://===== Doing nothing =====

                mainTarget = TargetCheckInView();

                if (mainTarget)
                {
                    AIStateCurrent = AIStates.Alerted;
                    break;
                }

                break;
            case AIStates.Alerted://===== In player's screen =====

                if (!TargetCheckInView())// If no targets are in view
                {
                    AIStateCurrent = AIStates.Idle;
                    break;
                }

                if (enemyMovement.CheckFront())
                {
                    AIStateCurrent = AIStates.AlertedDucked;
                    break;
                }

                TargetLookTowardMainTarget();


                switch (TargetCheckSamePlatformLine())
                {
                    case TargetVecticalPosition.Same:

                        AIStateCurrent = AIStates.Shooting;

                        break;
                    case TargetVecticalPosition.Above:

                        AIStateCurrent = AIStates.RePositioning;

                        break;
                    case TargetVecticalPosition.Under:

                        AIStateCurrent = AIStates.RePositioning;

                        break;
                }

                break;
            case AIStates.AlertedDucked://===== Alerted but ducked =====

                if (!TargetCheckInView())
                {
                    AIStateCurrent = AIStates.Idle;
                    break;
                }

                // TODO: Choose to run or shoot after some time

                break;
            case AIStates.Staggered://===== Hit by player =====

                break;
            case AIStates.Shooting://===== Just shooting =====

                if (TargetCheckSamePlatformLine() != TargetVecticalPosition.Same)
                    AIStateCurrent = AIStates.RePositioning;

                break;
            case AIStates.Charging://===== Running at the player =====

                break;
            case AIStates.RePositioning://===== Moving to another (better point) =====

                switch (TargetCheckSamePlatformLine())
                {
                    case TargetVecticalPosition.Same:// If the same go back to the AIStatePrevious

                        AIStateCurrent = AIStatePrevious;

                        break;
                    case TargetVecticalPosition.Above:

                        if (enemyMovement.CheckIfAbove())
                            enemyMovement.Jump();
                        else
                            enemyMovement.Move(TargetCheckDistance(mainTarget.transform.position).x);

                        break;
                    case TargetVecticalPosition.Under:

                        enemyMovement.Move(TargetCheckDistance(mainTarget.transform.position).x);

                        //AIStateCurrent = AIStates.RePositioning;

                        break;
                }

                break;
        }
    }

    private IEnumerator WaitForAction(AIStates _state, float _timeSec)
    {
        crIsRunning = true;

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

        crIsRunning = false;
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
    /// <returns>The object that has been spotted</returns>
    private PlayerController TargetCheckInView()
    {
        PlayerController _return = null;

        foreach (PlayerController _object in Targets)
        {
            if (Mathf.Abs(TargetCheckDistance(_object.transform.position).x) <= 18 - 3)
            {
                _return = _object;
                break;
            } 
        }
        return _return;
    }

    /// <summary>
    /// Flips the object toward the mainTarget
    /// </summary>
    private void TargetLookTowardMainTarget()
    {
        if (!mainTarget)
            return;

        float _distance = TargetCheckDistance(mainTarget.transform.position).x;

        if (_distance > 0)// If player is on the right
        {
            enemyMovement.FlipCharacter(true);
        }
        else if (_distance < 0)// If player is on the left
        {
            enemyMovement.FlipCharacter(false);
        }
    }

    /// <summary>
    /// Checks if the target is Above the object, Bellow or at the Same level
    /// </summary>
    /// <returns></returns>
    private TargetVecticalPosition TargetCheckSamePlatformLine()
    {
        TargetVecticalPosition _return = TargetVecticalPosition.Same;

        if (mainTarget.PlatformLine > enemyController.PlatformLine)
            _return = TargetVecticalPosition.Above;
        else if (mainTarget.PlatformLine < enemyController.PlatformLine)
            _return = TargetVecticalPosition.Under;

        return _return;
    }

    private Vector2 TargetCheckDistance(Vector2 _targetPosition)
    {
        return new Vector2(_targetPosition.x - transform.position.x, _targetPosition.y - transform.position.y);
    }

    /// <summary>
    /// Get all the targets (Gameobjects with the Player tag)
    /// </summary>
    /// <returns></returns>
    public List<PlayerController> TargetUpdateList()
    {
        // Clear the list
        Targets.Clear();

        // Find all the game
        foreach (GameObject _object in GameObject.FindGameObjectsWithTag("Player"))
        {
            Targets.Add(_object.GetComponent<PlayerController>());
        }

        return Targets;
    }
}
