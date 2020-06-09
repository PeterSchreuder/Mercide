using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScoreManager : MonoBehaviour
{
    private Action<EventParam> updateListenerEnemy;
    private Action<EventParam> gameStateListener;

    private int enemyAmountTotal;

    private float scoreAmount;
    public float ScoreAmount
    {
        get => scoreAmount;

        set
        {
            scoreAmount = value;
            SendScore();
        }
            
    }

    private int killAmount = 0;
    public int KillAmount
    {
        get => killAmount;
        set
        {
            killAmount = value;
            EventManager.TriggerEvent("EntityPlayer:UpdateKills", new EventParam { Int = KillAmount });
        }
    }

    void SendScore()
    {

        EventManager.TriggerEvent("EntityPlayer:UpdateScore", new EventParam { Float = ScoreAmount });
    }

    // Start is called before the first frame update
    void Awake()
    {
        updateListenerEnemy = new Action<EventParam>(UpdateKillAmount);
        gameStateListener = new Action<EventParam>(UpdateByGameState);
    }

    private void Start()
    {
        // Get the amount of enemies
        enemyAmountTotal = GameObject.FindGameObjectsWithTag("Enemy").Length;

        // Zero the score
        UpdateScoreAmountFloat(0);
        UpdateKillAmount(0);
    }

    // - Start listening
    void OnEnable()
    {
        EventManager.StartListening("EntityEnemy:Died", UpdateKillAmount);
        EventManager.StartListening("GameManager:ChangeState", UpdateByGameState);
    }

    // - Stop listening
    void OnDisable()
    {
        EventManager.StopListening("EntityEnemy:Died", UpdateKillAmount);
        EventManager.StopListening("GameManager:ChangeState", UpdateByGameState);
    }

    //void UpdateScoreAmount(float _amount)
    //{
    //    EventManager.TriggerEvent("EntityPlayer:GotScore", new EventParam { Float = _amount });
    //}

    /// <summary>
    /// Updates when the GameStateCurrent changes
    /// </summary>
    /// <param name="_data"></param>
    void UpdateByGameState(EventParam _data)
    {
        if (_data.GameState == GameStates.Mid || _data.GameState == GameStates.Win)
        {
            // Send the data
            EventManager.TriggerEvent("EntityPlayer:UpdateScore", new EventParam { Float = ScoreAmount });
            
        }

        // Send kill amount on win
        if (_data.GameState == GameStates.Win)
        {
            EventManager.TriggerEvent("EntityPlayer:UpdateKills", new EventParam { Int = KillAmount, Int2 = enemyAmountTotal, Bool = true });

            // After the Win dont count the kills anymore
            EventManager.StopListening("EntityEnemy:Died", UpdateKillAmount);
        }
    }

    #region Kills

    void UpdateKillAmount(int _amount)
    {
        KillAmount += _amount;
        UpdateScoreAmountFloat(_amount * 100f);
    }

    void UpdateKillAmount(EventParam _data)
    {
        KillAmount += 1;
        UpdateScoreAmountFloat(_data.Float);
    }

    #endregion

    #region Score

    void UpdateScoreAmountFloat(float _amount)
    {
        ScoreAmount += _amount;

    }

    void UpdateScoreAmount(EventParam _data)
    {
        ScoreAmount += _data.Float;
    }

    #endregion
}
