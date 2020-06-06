using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    protected enum MoveVerticalStates { Noone, Jumping, Grounded, Crouched };
    protected MoveVerticalStates MoveVerticalStateCurrent = MoveVerticalStates.Noone;

    public float moveSpeed = 5;
    public float jumpForce = 650;

    public Transform head;
    public Transform feet;
    public LayerMask groundObjects;
    public float checkRadius;
    public int maxJumpCount = 1;

    protected bool isJumping = false;

    protected int jumpCount = 0;


    protected Rigidbody2D rb;

    protected bool facingRight = true;
    protected float moveDirection;

    protected bool isGrounded = false;

    protected float distToFeet;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        distToFeet = gameObject.GetComponent<Collider2D>().bounds.extents.y + 0.1f;
    }

    protected virtual void FixedUpdate()
    {
        isGrounded = CheckIfGrounded();
    }

    protected bool CheckIfGrounded()
    {
        bool _return = Physics2D.OverlapCircle(feet.position, checkRadius, groundObjects);//Physics.Raycast(transform.position, -Vector3.up, -distToFeet);

        return _return;
    }

    protected void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    protected virtual void Animate()
    {
        if (moveDirection > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (moveDirection < 0 && facingRight)
        {
            FlipCharacter();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(head.position, checkRadius);
        Gizmos.DrawSphere(feet.position, checkRadius);
    }
}
