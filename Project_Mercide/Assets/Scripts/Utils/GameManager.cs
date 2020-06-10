using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStates { Noone, Begin, Mid, Win, Lose };

public class GameManager : MonoBehaviour
{
    private Action<EventParam> changeStateListener;

    private Action<EventParam> playerDeathListener;

    // Debugmode
    [SerializeField]
    private bool debugEnabled = true;
    public bool DebugEnabled { get => debugEnabled; set => debugEnabled = value; }

    private bool crIsRunning = false;

    private GameStates gameStateCurrent;
    public GameStates GameStateCurrent
    {
        get => gameStateCurrent;

        set
        {
            GameStatePrevious = GameStateCurrent;
            gameStateCurrent = value;

            switch (GameStateCurrent)
            {
                case GameStates.Begin:// At the beginning of the game (When restarted)

                    EventManager.TriggerEvent("UIScreen:Open", new EventParam { UIScreenType = UIScreenTypes.Begin });

                    // Setup the game
                    if (DebugEnabled || Debug.isDebugBuild)
                        EventManager.TriggerEvent("UIScreen:Open", new EventParam { UIScreenType = UIScreenTypes.Debug });
                    

                    if (!crIsRunning)
                        StartCoroutine(WaitAfterState(GameStates.Begin, 1.25f));

                    //GameStateCurrent = GameStates.Mid;

                    break;

                case GameStates.Mid:

                    EventManager.TriggerEvent("UIScreen:Close", new EventParam { UIScreenType = UIScreenTypes.Begin });

                    EventManager.TriggerEvent("UIScreen:Open", new EventParam { UIScreenType = UIScreenTypes.Mid });
                    EventManager.TriggerEvent("UIScreen:Open", new EventParam { UIScreenType = UIScreenTypes.Input });

                    break;

                case GameStates.Win:

                    EventManager.TriggerEvent("UIScreen:Close", new EventParam { UIScreenType = UIScreenTypes.Mid });
                    EventManager.TriggerEvent("UIScreen:Close", new EventParam { UIScreenType = UIScreenTypes.Input });

                    EventManager.TriggerEvent("UIScreen:Open", new EventParam { UIScreenType = UIScreenTypes.Win });

                    break;

                case GameStates.Lose:

                    EventManager.TriggerEvent("UIScreen:Close", new EventParam { UIScreenType = UIScreenTypes.Mid });
                    EventManager.TriggerEvent("UIScreen:Close", new EventParam { UIScreenType = UIScreenTypes.Input });

                    EventManager.TriggerEvent("UIScreen:Open", new EventParam { UIScreenType = UIScreenTypes.Lose });

                    break;
            }
        }
    }

    private GameStates gameStatePrevious;
    public GameStates GameStatePrevious
    {
        get => gameStatePrevious;

        set
        {
            gameStatePrevious = value;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 60;

        GameStateCurrent = GameStates.Begin;

        changeStateListener = new Action<EventParam>(EventUpdateGameState);
        playerDeathListener = new Action<EventParam>(PlayerDied);
    }

    void OnEnable()
    {
        //Register With Action variable
        EventManager.StartListening("GameManager:ChangeState", EventUpdateGameState);
        EventManager.StartListening("EntityPlayer:Died", PlayerDied);
    }

    void OnDisable()
    {
        //Un-Register With Action variable
        EventManager.StopListening("GameManager:ChangeState", EventUpdateGameState);
        EventManager.StopListening("EntityPlayer:Died", PlayerDied);
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameStateCurrent)
        {
            case GameStates.Begin:

                break;

            case GameStates.Mid:

                break;

            case GameStates.Win:

                break;
            case GameStates.Lose:

                break;
        }
    }

    /// <summary>
    /// Wait after this state to run the code in the current state
    /// </summary>
    /// <param name="_state">Current state</param>
    /// <param name="_timeSec">Secconds to wait</param>
    /// <returns></returns>
    private IEnumerator WaitAfterState(GameStates _state, float _timeSec)
    {
        crIsRunning = true;

        yield return new WaitForSeconds(_timeSec);

        switch (_state)
        {
            case GameStates.Begin:

                GameStateCurrent = GameStates.Mid;

                break;
            case GameStates.Mid:

                break;
            case GameStates.Win:

                break;
            case GameStates.Lose:

                break;
        }

        crIsRunning = false;
    }

    /// <summary>
    /// Update the GameStateCurrent through an Event
    /// </summary>
    /// <param name="_data">gameState</param>
    public void EventUpdateGameState(EventParam _data)
    {
        // Check if the variable is assigned
        if (_data.GameState != GameStates.Noone)
            GameStateCurrent = _data.GameState;
    }

    public void PlayerDied(EventParam _data)
    {
        GameStateCurrent = GameStates.Lose;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
