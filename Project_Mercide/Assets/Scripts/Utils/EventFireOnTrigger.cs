using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventFireOnTrigger : MonoBehaviour
{
    [SerializeField]
    private string tagOfTarget = "Player";

    [SerializeField]
    private string eventName = "ClassOfSource:Test";

    [SerializeField]
    private EventParam dataToSend = new EventParam();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagOfTarget))
            EventManager.TriggerEvent(eventName, dataToSend);
    }
}
