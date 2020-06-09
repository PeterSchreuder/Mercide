using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManagerMidGame : UIManager
{
    [SerializeField]
    private Text health, score;

    private Action<EventParam> uiUpdateListenerHealth;
    private Action<EventParam> uiUpdateListenerScore;

    // Start is called before the first frame update
    void Awake()
    {
        uiUpdateListenerHealth = new Action<EventParam>(TextUpdateHealth);
        uiUpdateListenerScore = new Action<EventParam>(TextUpdateScore);
    }

    // - Start listening
    void OnEnable()
    {
        EventManager.StartListening("EntityPlayer:UpdateHealth", TextUpdateHealth);

        EventManager.StartListening("EntityPlayer:GotHealth", TextUpdateHealth);
        EventManager.StartListening("EntityPlayer:GotDamage", TextUpdateHealth);

        EventManager.StartListening("EntityPlayer:UpdateScore", TextUpdateScore);

        EventManager.StartListening("EntityPlayer:GotScore", TextUpdateScore);
        EventManager.StartListening("EntityPlayer:LostScore", TextUpdateScore);
    }

    // - Stop listening
    void OnDisable()
    {
        EventManager.StopListening("EntityPlayer:UpdateHealth", TextUpdateHealth);

        EventManager.StopListening("EntityPlayer:GotHealth", TextUpdateHealth);
        EventManager.StopListening("EntityPlayer:GotDamage", TextUpdateHealth);

        EventManager.StopListening("EntityPlayer:UpdateScore", TextUpdateScore);

        EventManager.StopListening("EntityPlayer:GotScore", TextUpdateScore);
        EventManager.StopListening("EntityPlayer:LostScore", TextUpdateScore);
    }

    void TextUpdateHealth(EventParam _data)
    {
        print("health");
        UpdateText(health, "Health: " + _data.Float.ToString());
    }

    void TextUpdateScore(EventParam _data)
    {
        print("score");
        UpdateText(score, _data.Float.ToString() + " :Score");
    }
}
