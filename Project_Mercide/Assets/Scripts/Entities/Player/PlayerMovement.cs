using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : EntityMovement
{
    public float moveSpeed;
    public float jumpForce;
    public int maxJumpCount = 1;

    private Rigidbody2D rb;
    private bool facingRight = true;
    private float moveDirection;
    private bool isJumping = false;

    private int jumpCount;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void Start()
    {
        base.Start();

        jumpCount = maxJumpCount;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();

        Animate();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (isGrounded)
        {
            jumpCount = maxJumpCount;
        }

        Move();
    }

    private void ProcessInput()
    {
        moveDirection = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            isJumping = true;
            print(1);
        }
    }

    private void Animate()
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

    private void Move()
    {
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        if (isJumping && jumpCount > 0)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            jumpCount--;
            isJumping = false;
        }
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
