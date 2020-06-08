using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates { Begin, Mid, Win, Lose };

public class GameManager : MonoBehaviour
{
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
                        StartCoroutine(WaitAfterState(GameStates.Begin, 2f));

                    //GameStateCurrent = GameStates.Mid;

                    break;

                case GameStates.Mid:

                    EventManager.TriggerEvent("UIScreen:Close", new EventParam { UIScreenType = UIScreenTypes.Begin });

                    EventManager.TriggerEvent("UIScreen:Open", new EventParam { UIScreenType = UIScreenTypes.Mid });
                    EventManager.TriggerEvent("UIScreen:Open", new EventParam { UIScreenType = UIScreenTypes.Input });

                    break;

                case GameStates.Win:

                    break;

                case GameStates.Lose:

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
}
