using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManagerMidGame : UIManager
{
    [SerializeField]
    private Text health, score;

    [SerializeField]
    private Image hitFlashScreen;

    private Action<EventParam> uiUpdateListenerHealth;
    private Action<EventParam> uiUpdateListenerScore;

    private Action<EventParam> playerHitListener;

    // Start is called before the first frame update
    void Awake()
    {
        uiUpdateListenerHealth = new Action<EventParam>(TextUpdateHealth);
        uiUpdateListenerScore = new Action<EventParam>(TextUpdateScore);

        playerHitListener = new Action<EventParam>(FeedbackShowHitFlash);
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

        EventManager.StartListening("EntityPlayer:GotDamage", FeedbackShowHitFlash);
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

        EventManager.StopListening("EntityPlayer:GotDamage", FeedbackShowHitFlash);
    }

    void FeedbackShowHitFlash(EventParam _data)
    {
        Color _tempColor = hitFlashScreen.color;
        _tempColor.a = 1f;

        hitFlashScreen.color = _tempColor;
    }

    void TextUpdateHealth(EventParam _data)
    {
        UpdateText(health, "Health: " + _data.Float.ToString());
    }

    void TextUpdateScore(EventParam _data)
    {
        UpdateText(score, _data.Float.ToString() + " :Score");
    }
}
