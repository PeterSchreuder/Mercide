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

    [SerializeField]
    private int platformLine;
    public int Platformline { get => platformLine; set => platformLine = value; }

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

        // If not grounded. Update the platform line
        //if (rb.velocity.y != 0)
            UpdatePlatformLineGrounded();
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    // Updates the PlatformLine when grouded
    void UpdatePlatformLineGrounded()
    {
        if (rb)
        {
            if (rb.velocity.y == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (transform.position.y >= i * 4.5)
                        Platformline = i;
                    else
                        break;
                }
            }
        }
        else
            Debug.LogError("No rigidbody found");
    }
}
