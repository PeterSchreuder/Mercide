using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityHealthStates { Alive, Dead, Invincible };

public class GlobalObject : MonoBehaviour
{
    [SerializeField]
    private int teamNumber;
    public int TeamNumber { get => teamNumber; set => teamNumber = value; }

    private int objectId;
    public int ObjectId { get => objectId; set => objectId = value; }

    [HideInInspector]
    public GlobalObject lastHitter;

    //- Health
    private float health;
    public float Health
    {
        get => health;
        set
        {
            // Fire an event that the object lost health
            EventManager.TriggerEvent("Entity" + gameObject.tag + ":UpdateHealth", new EventParam { Float = Health, Float2 = value });
            health = value;
        }
    }

    private EntityHealthStates healthStateCurrent;
    public EntityHealthStates HealthStateCurrent { get => healthStateCurrent; set => healthStateCurrent = value; }

    //- Current Platform line
    [SerializeField]
    private int platformLine;
    public int PlatformLine { get => platformLine; set => platformLine = value; }

    [SerializeField]
    private int platformLineInAir;
    public int PlatformLineInAir { get => platformLineInAir; set => platformLineInAir = value; }

    protected float deltaTime;
    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        Health = 100f;
    }

    protected virtual void FixedUpdate()
    {
        deltaTime = Time.deltaTime;

        // Update the PlatformLine variables
        UpdatePlatformLine();
        UpdatePlatformLineGrounded();
    }

    public virtual void Die()
    {
        HealthStateCurrent = EntityHealthStates.Dead;

        EventManager.TriggerEvent("Entity" + gameObject.tag + ":Died", new EventParam { Float = 100 });
        Destroy(gameObject);
    }

    /// <summary>
    /// Check if the Object is Dead via a TernaryOperator
    /// </summary>
    /// <returns></returns>
    public virtual bool CheckIfDead()
    {
        return HealthStateCurrent == EntityHealthStates.Dead ? true : false;
    }

    /// <summary>
    /// Updates PlatformLine only when velocity.y == 0
    /// </summary>
    void UpdatePlatformLineGrounded()
    {
        if (rb)
        {
            if (rb.velocity.y == 0)
            {
                PlatformLine = UpdatePlatformLine();
            }
        }
    }

    /// <summary>
    /// Updates the PlatformLineInAir
    /// </summary>
    /// <returns>PlatformLineInAir</returns>
    int UpdatePlatformLine()
    {
        if (rb)
        {
            for (int i = 0; i < 4; i++)
            {
                if (transform.position.y >= (i * 4f) + 0.5f)
                    PlatformLineInAir = i;
                else
                    break;
            }
        }

        return PlatformLineInAir;
    }
}
