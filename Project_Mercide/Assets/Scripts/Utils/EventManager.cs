using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/* -= How to use =-

    - Listen to an event:
    private Action<EventParam> testListener;

    // - Define
    void Awake()
    {
        testListener = new Action<EventParam>(TestFunction);
    }

    // - Start listening
    void OnEnable()
    {
        EventManager.StartListening("ClassOfSource:Test", TestFunction);
        EventManager.StartListening("ClassOfSource:Test", TestFunction2);
    }

    // - Stop listening
    void OnDisable()
    {
        EventManager.StopListening("ClassOfSource:Test", TestFunction);
        EventManager.StopListening("ClassOfSource:Test", TestFunction2);
    }

    // - Function to use the event data
    void TestFunction(EventParam _values)
    {
        
    }

    - Trigger an event:
    EventManager.TriggerEvent("ClassOfSource:Test");// Trigger an event
    EventManager.TriggerEvent("ClassOfSource:Test", new EventParam { UIScreenType = UIScreenTypes.Mid });// Trigger an event and give it data

    -= Made possible by:
    https://learn.unity.com/tutorial/create-a-simple-messaging-system-with-events#5cf5960fedbc2a281acd21fa
    and: https://stackoverflow.com/a/42034899

*/

public class EventManager : MonoBehaviour
{
    private Dictionary<string, Action<EventParam>> eventDictionary;

    private static EventManager eventManager;

    // Instance getter
    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("No active EventManager found!");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Init()
    {
        // Create a new Dictionary
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, Action<EventParam>>();
        }
    }

    /// <summary>
    /// Start listening to an Event
    /// </summary>
    /// <param name="_eventName"></param>
    /// <param name="_listener"></param>
    public static void StartListening(string _eventName, Action<EventParam> _listener)
    {
        Action<EventParam> _thisEvent = null;

        // Start listening to the event by adding a listener to an event in the Dictionary
        if (instance.eventDictionary.TryGetValue(_eventName, out _thisEvent))
        {
            _thisEvent += _listener;

            instance.eventDictionary[_eventName] = _thisEvent;
        }
        else // If the event does not exist, create one
        {
            _thisEvent += _listener;
            instance.eventDictionary.Add(_eventName, _thisEvent);
        }
    }

    /// <summary>
    /// Stop listening to an Event
    /// </summary>
    /// <param name="_eventName"></param>
    /// <param name="_listener"></param>
    public static void StopListening(string _eventName, Action<EventParam> _listener)
    {
        if (eventManager == null)
            return;

        Action<EventParam> _thisEvent = null;

        // Remove the listener from the event
        if (instance.eventDictionary.TryGetValue(_eventName, out _thisEvent))
        {
            _thisEvent -= _listener;

            instance.eventDictionary[_eventName] = _thisEvent;
        }
    }

    /// <summary>
    /// Trigger an Event
    /// </summary>
    /// <param name="_eventName"></param>
    public static void TriggerEvent(string _eventName, EventParam _eventParam = default)
    {
        Action<EventParam> _thisEvent = null;

        // Trigger the event by getting it from the Dictionary and Invoking it
        if (instance.eventDictionary.TryGetValue(_eventName, out _thisEvent))
        {
            _thisEvent.Invoke(_eventParam);
        }
    }
}

// Parameter structure class
public struct EventParam
{
    public UIScreenTypes UIScreenType;
    public string String;
    public int Int;
    public float Float;
    public bool Bool;
}