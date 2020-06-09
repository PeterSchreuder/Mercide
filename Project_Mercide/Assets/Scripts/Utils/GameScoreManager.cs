using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScoreManager : MonoBehaviour
{
    private Action<EventParam> updateListenerEnemy;
    private Action<EventParam> gameStateListener;

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
    public int KillAmount { get => killAmount; set => killAmount = value; }

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
        //UpdateScoreAmountFloat(0);
        //UpdateKillAmount(0);
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
            print("Update");
            // Send the data
            EventManager.TriggerEvent("EntityPlayer:UpdateScore", new EventParam { Float = ScoreAmount });
            EventManager.TriggerEvent("EntityPlayer:UpdateKills", new EventParam { Int = KillAmount });
        }
    }

    #region Kills

    void UpdateKillAmount(int _amount)
    {
        KillAmount += _amount;
        EventManager.TriggerEvent("EntityPlayer:UpdateKills", new EventParam { Int = KillAmount });
        UpdateScoreAmountFloat(100f);
    }

    void UpdateKillAmount(EventParam _data)
    {
        KillAmount += _data.Int;
        EventManager.TriggerEvent("EntityPlayer:UpdateKills", new EventParam { Int = KillAmount });
        UpdateScoreAmountFloat(100f);
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
