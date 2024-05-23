using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlayer : MonoBehaviour
{
    public float MoveSpeed;
    public float runSpeed;
    public float JumpShort;
    public float JumpLong;
    public float TimeJump = 0.1f;

    private Rigidbody2D _compRigidbody2D;
    private bool isRunning = false;
    private bool isJumping = false;
    public float TimeCounterJump;
    private void Awake()
    {
        _compRigidbody2D = GetComponent<Rigidbody2D>();
        TimeCounterJump = TimeJump;
    }
  
    void Update()
    {
      
        float moveInput = Input.GetAxisRaw("Horizontal");


        float speed = isRunning ? runSpeed: runSpeed * 0.5f;

        if (moveInput != 0)
        {
            _compRigidbody2D.AddForce(new Vector2(moveInput * MoveSpeed, 0));
        }

        Vector2 clampedVelocity = _compRigidbody2D.velocity;
        clampedVelocity.x = Mathf.Clamp(clampedVelocity.x, -MoveSpeed, MoveSpeed);
        _compRigidbody2D.velocity = clampedVelocity;


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            TimeCounterJump = TimeJump;
            _compRigidbody2D.AddForce(Vector2.up * JumpShort, ForceMode2D.Impulse);
        }

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) && isJumping)
        {
            if (TimeCounterJump > 0)
            {
                _compRigidbody2D.AddForce(Vector2.up * JumpLong, ForceMode2D.Impulse);
                TimeCounterJump -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }

}
