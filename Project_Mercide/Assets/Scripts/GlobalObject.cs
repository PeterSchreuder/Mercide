using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalObject : MonoBehaviour
{
    [SerializeField]
    private int teamNumber;
    public int TeamNumber { get => teamNumber; set => teamNumber = value; }

    private float health;
    public float Health { get => health; set => health = value; }

    protected float deltaTime;

    protected virtual void Start()
    {
        Health = 100f;
    }

    protected virtual void FixedUpdate()
    {
        deltaTime = Time.deltaTime;
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
