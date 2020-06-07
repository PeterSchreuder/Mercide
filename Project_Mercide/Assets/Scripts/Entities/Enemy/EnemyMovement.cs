using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : EntityMovement
{
    private Action<EventParam> inputListener;

    protected override void Awake()
    {
        base.Awake();

        inputListener = new Action<EventParam>(ProcessInput);
    }

    void OnEnable()
    {
        EventManager.StartListening("InputManager:Input", ProcessInput);
    }

    // - Stop listening
    void OnDisable()
    {
        EventManager.StopListening("ClassOfSource:Input", ProcessInput);
    }

    protected override void Start()
    {
        base.Start();

        jumpCount = maxJumpCount;
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

    private void ProcessInput(EventParam _input)// int = horizontal, bool = jump
    {
        moveDirection = _input.Float;//Input.GetAxis("Horizontal");
                                   //Input.GetButtonDown("Jump")

        if (_input.Bool && jumpCount > 0)
        {
            isJumping = true;
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2((moveDirection * moveSpeed) * deltaTime, rb.velocity.y);

        if (isJumping && jumpCount > 0)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            jumpCount--;
        }

        // Reset the values
        isJumping = false;

        Animate();
        moveDirection = 0;

    }

    
}
