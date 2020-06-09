using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerWinGame : UIManager
{
    [SerializeField]
    private Text score, kills, message;

    private Action<EventParam> uiUpdateListenerScore;
    private Action<EventParam> uiUpdateListenerKills;

    // Start is called before the first frame update
    void Awake()
    {
        uiUpdateListenerScore = new Action<EventParam>(TextUpdateScore);
        uiUpdateListenerKills = new Action<EventParam>(TextUpdateScore);

        //EventManager.StartListening("EntityPlayer:UpdateScore", TextUpdateScore);
    }

    // - Stop listening
    void OnEnable()
    {
        EventManager.StartListening("EntityPlayer:UpdateScore", TextUpdateScore);
    }

    // - Stop listening
    void OnDisable()
    {
        EventManager.StopListening("EntityPlayer:UpdateScore", TextUpdateScore);
    }

    void TextUpdateScore(EventParam _data)
    {
        UpdateText(score, "Score: " + _data.Float.ToString());
    }

    void TextUpdateKills(EventParam _data)
    {
        UpdateText(score, "Kills: " + _data.Int.ToString());
    }
}
