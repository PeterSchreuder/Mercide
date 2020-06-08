using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MidGameUIManager : MonoBehaviour
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

        EventManager.StartListening("EntityPlayer:GotScore", TextUpdateScore);
        EventManager.StartListening("EntityPlayer:LostScore", TextUpdateScore);
    }

    // - Stop listening
    void OnDisable()
    {
        EventManager.StopListening("EntityPlayer:UpdateHealth", TextUpdateHealth);

        EventManager.StopListening("EntityPlayer:GotHealth", TextUpdateHealth);
        EventManager.StopListening("EntityPlayer:GotDamage", TextUpdateHealth);

        EventManager.StopListening("EntityPlayer:GotScore", TextUpdateScore);
        EventManager.StopListening("EntityPlayer:LostScore", TextUpdateScore);
    }

    void TextUpdateHealth(EventParam _data)
    {
        UpdateText(health, "Health: " + _data.Float.ToString());
    }

    void TextUpdateScore(EventParam _data)
    {
        UpdateText(score, _data.Float.ToString() + " :Score");
    }

    void UpdateText(Text _text, string _value)
    {
        _text.text = _value;
    }
}
