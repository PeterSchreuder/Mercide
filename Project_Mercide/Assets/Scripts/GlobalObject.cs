using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalObject : MonoBehaviour
{
    [SerializeField]
    private int teamNumber;
    public int TeamNumber { get => teamNumber; set => teamNumber = value; }

    private int objectId;
    public int ObjectId { get => objectId; set => objectId = value; }

    private float health;
    public float Health { get => health; set => health = value; }

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
        Destroy(gameObject);
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
                PlatformLineInAir = UpdatePlatformLine();
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
