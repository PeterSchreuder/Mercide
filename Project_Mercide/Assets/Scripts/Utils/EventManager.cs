using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/* -= How to use =-

    - Listen to an event:
    private UnityAction testListener;

    // - Define
    void Awake()
    {
        testListener = new UnityAction(TestFunction);
    }

    // - Start listening
    void OnEnable()
    {
        EventManager.StartListening("ClassOfSource:Test", testListener);
    }

    // - Stop listening
    void OnDisable()
    {
        EventManager.StopListening("ClassOfSource:Test", testListener);
    }

    - Trigger an event:
    EventManager.TriggerEvent("ClassOfSource:Test");

    -= Made possible by:
    https://learn.unity.com/tutorial/create-a-simple-messaging-system-with-events#5cf5960fedbc2a281acd21fa
 
*/

public class EventManager : MonoBehaviour
{
    private Dictionary<string, UnityEvent> eventDictionary;

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
            eventDictionary = new Dictionary<string, UnityEvent>();
        }
    }

    /// <summary>
    /// Start listening to an Event
    /// </summary>
    /// <param name="_eventName"></param>
    /// <param name="_listener"></param>
    public static void StartListening(string _eventName, UnityAction _listener)
    {
        UnityEvent _thisEvent = null;

        // Start listening to the event by adding a listener to an event in the Dictionary
        if (instance.eventDictionary.TryGetValue(_eventName, out _thisEvent))
        {
            _thisEvent.AddListener(_listener);
        }
        else // If the event does not exist, create one
        {
            _thisEvent = new UnityEvent();
            _thisEvent.AddListener(_listener);
            instance.eventDictionary.Add(_eventName, _thisEvent);
        }
    }

    /// <summary>
    /// Stop listening to an Event
    /// </summary>
    /// <param name="_eventName"></param>
    /// <param name="_listener"></param>
    public static void StopListening(string _eventName, UnityAction _listener)
    {
        if (eventManager == null)
            return;

        UnityEvent _thisEvent = null;

        // Remove the listener from the event
        if (instance.eventDictionary.TryGetValue(_eventName, out _thisEvent))
        {
            _thisEvent.RemoveListener(_listener);
        }
    }

    /// <summary>
    /// Trigger an Event
    /// </summary>
    /// <param name="_eventName"></param>
    public static void TriggerEvent(string _eventName)
    {
        UnityEvent _thisEvent = null;

        // Trigger the event by getting it from the Dictionary and Invoking it
        if (instance.eventDictionary.TryGetValue(_eventName, out _thisEvent))
        {
            _thisEvent.Invoke();
        }
    }
}
