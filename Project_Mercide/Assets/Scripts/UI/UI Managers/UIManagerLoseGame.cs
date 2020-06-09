using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManagerLoseGame : UIManager
{
    [SerializeField]
    private Text score;

    private Action<EventParam> uiUpdateListenerScore;

    // Start is called before the first frame update
    void Awake()
    {
        uiUpdateListenerScore = new Action<EventParam>(TextUpdateScore);
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
}
