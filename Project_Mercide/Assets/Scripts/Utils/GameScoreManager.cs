using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScoreManager : MonoBehaviour
{
    private Action<EventParam> updateListenerScore;

    private float scoreAmount = 0;
    public float ScoreAmount { get => scoreAmount; set => scoreAmount = value; }

    // Start is called before the first frame update
    void Awake()
    {
        updateListenerScore = new Action<EventParam>(UpdateScore);
    }

    private void Start()
    {
        UpdateScore(new EventParam { Float = 0 });
    }

    // - Start listening
    void OnEnable()
    {
        EventManager.StartListening("EntityEnemy:Died", UpdateScore);
    }

    // - Stop listening
    void OnDisable()
    {
        EventManager.StopListening("EntityEnemy:Died", UpdateScore);
    }

    //void UpdateScore(float _amount)
    //{
    //    EventManager.TriggerEvent("EntityPlayer:GotScore", new EventParam { Float = _amount });
    //}

    void UpdateScore(EventParam _data)
    {
        print("Score");
        ScoreAmount += _data.Float;
        EventManager.TriggerEvent("EntityPlayer:GotScore", new EventParam { Float = ScoreAmount });
    }
}
