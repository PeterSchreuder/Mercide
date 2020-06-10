using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerTypes { Default, PickupHealth };

public class EventFireOnTrigger : MonoBehaviour
{
    [SerializeField]
    private TriggerTypes triggerType;

    [SerializeField]
    private string tagOfTarget = "Player";

    [SerializeField]
    private string eventName = "ClassOfSource:Test";

    [SerializeField]
    private bool deleteOnTrigger = false;

    [SerializeField]
    private EventParam dataToSend = new EventParam();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagOfTarget))
        {
            bool _send = true;

            if (triggerType == TriggerTypes.PickupHealth && collision.GetComponent<GlobalObject>().Health > 99)
            {
                _send = false;
            }

            if (_send)
            {
                EventManager.TriggerEvent(eventName, dataToSend);

                if (deleteOnTrigger)
                    Destroy(gameObject);
            }
        } 
    }
}
