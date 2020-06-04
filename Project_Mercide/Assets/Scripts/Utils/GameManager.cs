using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates { Begin, Mid, Win, Lose };

public class GameManager : MonoBehaviour
{
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

                    // Setup the game

                    GameStateCurrent = GameStates.Mid;

                    break;

                case GameStates.Mid:

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
}
