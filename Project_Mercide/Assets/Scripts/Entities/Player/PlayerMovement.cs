using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : EntityMovement
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

        Move(MoveDirection);
    }

    private void ProcessInput(EventParam _input)// int = horizontal, bool = jump
    {
        MoveDirection = _input.Float;//Input.GetAxis("Horizontal");
                                   //Input.GetButtonDown("Jump")

        if (_input.Bool && jumpCount > 0)
        {
            isJumping = true;
        }
    }

    public override void Move(float _direction)
    {
        rb.velocity = new Vector2((MoveDirection * moveSpeed) * deltaTime, rb.velocity.y);

        if (isJumping)
            Jump();

        // Reset the values
        isJumping = false;

        Animate();
        MoveDirection = 0;
    }

    
}
