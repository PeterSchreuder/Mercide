using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    protected enum MoveVerticalStates { Noone, Jumping, Grounded, Crouched };
    protected MoveVerticalStates MoveVerticalStateCurrent = MoveVerticalStates.Noone;

    public Transform feet;
    public LayerMask groundObjects;
    public float checkRadius;

    protected bool facingRight = true;
    protected float moveDirection;

    protected bool isGrounded = false;

    protected float distToFeet;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        distToFeet = gameObject.GetComponent<Collider2D>().bounds.extents.y + 0.1f;
    }

    protected virtual void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(feet.position, 1, groundObjects);//CheckIfGrounded();
    }

    protected bool CheckIfGrounded()
    {
        Debug.DrawLine(feet.position, feet.position.AddY(distToFeet), Color.red, 1);

        bool _return = Physics.Raycast(transform.position, -Vector3.up, -distToFeet);

        return _return;
    }

    protected void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
