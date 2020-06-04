using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalObject : MonoBehaviour
{
    private int teamNumber;
    public int TeamNumber { get => teamNumber; set => teamNumber = value; }

    private float health;
    public float Health { get => health; set => health = value; }


    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
