using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : EntityMovement
{
    private Action<EventParam> inputListener;

    public float moveSpeed;
    public float jumpForce;
    public int maxJumpCount = 1;

    private Rigidbody2D rb;
    private bool isJumping = false;

    private int jumpCount;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

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

    // Update is called once per frame
    void Update()
    {
        //ProcessInput();

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

    private void ProcessInput(EventParam _input)// int = horizontal, bool = jump
    {
        Debug.Log(_input.Bool);

        
        moveDirection = _input.Float;//Input.GetAxis("Horizontal");
                                   //Input.GetButtonDown("Jump")

        if (_input.Bool && jumpCount > 0)
        {
            isJumping = true;
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        if (isJumping && jumpCount > 0)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            jumpCount--;
            
        }

        // Reset the values
        isJumping = false;
        moveDirection = 0;

    }

    
}
