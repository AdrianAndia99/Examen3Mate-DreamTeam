    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float runSpeed = 4f;
    public float jumpForce = 5f;
    public float accelerationTime = 0.5f;
    public bool suelo;
    public bool doubleJump;
    public LayerMask layerMask;
    
    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;
    private float accelerationTimer = 0f;
    private float currentSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void OnMovement(InputAction.CallbackContext contex)
    {
        moveInput = contex.ReadValue<Vector2>().x;
    }
    public void OnJump(InputAction.CallbackContext contex)
    {
        if (contex.phase == InputActionPhase.Performed)
        {
            if (suelo || doubleJump)
            {
                if (!suelo)
                {
                    doubleJump = false;
                }
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }
    void Update()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down, Color.blue);
        if (Physics2D.Raycast(transform.position, Vector2.down, 1f, layerMask))
        {
            suelo = true;
            doubleJump = true;
        }
        else
        {
            suelo = false;
        }
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);

        moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput != 0)
        {
            accelerationTimer += Time.deltaTime;
            if (accelerationTimer >= accelerationTime)
            {
                currentSpeed = runSpeed;
            }
            else
            {
                currentSpeed = walkSpeed;
            }
        }
        else
        {
            accelerationTimer = 0f;
            currentSpeed = walkSpeed;
        }

        rb.AddForce(new Vector2(moveInput * currentSpeed, 0), ForceMode2D.Force);

        if (isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
