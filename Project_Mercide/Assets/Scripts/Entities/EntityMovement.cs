using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnvironmentTypes { Noone, Platform, Cover, Block, Wall, Floor };

public class EntityMovement : MonoBehaviour
{
    protected enum MoveVerticalStates { Noone, Jumping, Grounded, Crouched };

    private MoveVerticalStates moveVerticalStateCurrent = MoveVerticalStates.Noone;
    protected MoveVerticalStates MoveVerticalStateCurrent { get => moveVerticalStateCurrent; set => moveVerticalStateCurrent = value; }

    //- Public
    public float moveSpeed = 400;
    public float jumpForce = 650;

    public Transform head;
    public Transform feet;
    public Transform front;
    public LayerMask groundObjects;
    public float checkRadius = 0.2f;
    public int maxJumpCount = 1;

    //- Protected
    protected bool isJumping = false;
    protected int jumpCount = 0;
    protected Rigidbody2D rb;
    protected bool facingRight = true;

    private float moveDirection;
    public float MoveDirection { get => moveDirection; set => moveDirection = value; }

    protected bool isGrounded = false;
    protected float distToFeet;
    protected float deltaTime;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        distToFeet = gameObject.GetComponent<Collider2D>().bounds.extents.y + 0.1f;
    }

    protected virtual void Update()
    {
        deltaTime = Time.deltaTime;
    }

    protected virtual void FixedUpdate()
    {
        isGrounded = CheckIfGrounded();
    }

    public virtual void MoveStop()
    {
        rb.velocity = new Vector2();

        Animate();
        MoveDirection = 0;
    }

    public virtual void Move()
    {
        rb.velocity = new Vector2((MoveDirection * moveSpeed) * deltaTime, rb.velocity.y);

        Animate();
        MoveDirection = 0;
    }

    public virtual void Move(float _direction)
    {
        rb.velocity = new Vector2((_direction * moveSpeed) * deltaTime, rb.velocity.y);

        Animate();
        MoveDirection = 0;
    }

    public void Jump()
    {
        if (jumpCount > 0)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            jumpCount--;
        }
    }

    /// <summary>
    /// Check when velocity.y == 0 if there is an platform above the entity
    /// </summary>
    /// <returns></returns>
    public EnvironmentTypes CheckIfAbove()
    {
        return CheckFlank(head.position);

        //bool _return = false;

        //// If the velocity is 0 than the entity is on the ground for sure
        //if (rb.velocity.y == 0)
        //{
        //    Collider2D[] _collision = Physics2D.OverlapCircleAll(head.position, checkRadius);

        //    foreach (Collider2D _object in _collision)
        //    {
        //        _return = _object.CompareTag("Platform");

        //        if (_return)
        //            break;
        //    }
        //}

        //return _return;
    }

    public EnvironmentTypes CheckFront()
    {
        return CheckFlank(front.position);
    }

    /// <summary>
    /// Checks position without compairing Strings in a switch statement (Making it much more expendable)
    /// </summary>
    /// <returns></returns>
    public EnvironmentTypes CheckFlank(Vector2 _position)
    {
        EnvironmentTypes _return = EnvironmentTypes.Noone;

        // If the velocity is 0 than the entity is on the ground for sure
        if (rb.velocity.y == 0)
        {
            Collider2D[] _collision = Physics2D.OverlapCircleAll(_position, checkRadius);

            // Get the length of the EnvironmentTypes enum
            int _enumLength = System.Enum.GetValues(typeof(EnvironmentTypes)).Length;
            EnvironmentTypes _index;// Index in the enum to be turned to a String

            // Go through all the collided objects
            foreach (Collider2D _object in _collision)
            {
                // Loop through the EnvironmentTypes enum
                for (int i = 0; i < _enumLength; i++)
                {
                    _index = (EnvironmentTypes)i;// Typecast it to an Int

                    // Compare the _object tag to the EnvironmentTypes enum name
                    if (_object.tag == _index.ToString())
                    {
                        // Return the enum of the collided object
                        _return = (EnvironmentTypes)i;
                        break;
                    }
                }
            }
        }

        return _return;
    }

    /// <summary>
    /// Check when velocity.y == 0 if the object is grounded
    /// </summary>
    /// <returns></returns>
    public bool CheckIfGrounded()
    {
        bool _return = false;

        // If the velocity is 0 than the entity is on the ground for sure
        if (rb.velocity.y == 0)
            _return = Physics2D.OverlapCircle(feet.position, checkRadius, groundObjects);//Physics.Raycast(transform.position, -Vector3.up, -distToFeet);

        return _return;
    }

    /// <summary>
    /// Rotates the character around the Y axes
    /// </summary>
    /// <param name="_rightSide">true = right, false = left</param>
    public void FlipCharacter(bool _rightSide)
    {
        if (facingRight.Equals(_rightSide))// If the Entity is already facting that way
            return;

        facingRight = _rightSide;

        float rotation = transform.eulerAngles.y.Round();

        // Rotate and use _rightSide.ToFloat() to scale the added rotation from 180 * 0 to 180 * 1
        transform.Rotate(0f, 180f * _rightSide.ToFloat(), 0f);
    }

    public virtual void Animate()
    {
        if (moveDirection > 0 && !facingRight)// Left to Right
        {
            FlipCharacter(true);
        }
        else if (moveDirection < 0 && facingRight)// Right to Left
        {
            FlipCharacter(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(head.position, checkRadius);
        Gizmos.DrawSphere(feet.position, checkRadius);
        Gizmos.DrawSphere(front.position, checkRadius);
    }
}
