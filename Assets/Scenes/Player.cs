using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MovimientoSalto : MonoBehaviour
{
    public float speed;
    public float jumpforce;
    public int maxJumps = 2;
    int JumpsRemaining;
    public float check;
    private float moveInput;

    void Update()
    {
        transform.Translate(Vector2.right * moveInput * speed * Time.deltaTime);       
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>().x;
        Debug.Log("me muevo");
    }
}