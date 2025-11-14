using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpHeight;
    [SerializeField] private InputActionReference jump;
    [SerializeField] private InputActionReference dash;

    [SerializeField] public float gravity;
    Vector2 movement = new Vector2();
    public Rigidbody2D rb;
    bool isGrounded;

    public Vector3 moveDirection;
    public float maxDashTime = 1.0f;
    [SerializeField] public float dashSpeed = 5.0f;
    public float dashStoppingSpeed = 0.1f;

    private float currentDashTime;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        jump.action.performed += OnJump;
        dash.action.performed += OnDash;


    }

    private void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("We jump1.");
        if (isGrounded)
        {
            isGrounded = false;
            movement.y = context.ReadValue<float>();
        }
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        Debug.Log("We dash1.");
        currentDashTime = 0.0f;
    }

    private void Update()
    {
        if (!isGrounded)
        {
            movement.y -= gravity;
        }
        Vector2 position = transform.position;
        float xMovement = movement.x * Time.deltaTime * speed;
        if (currentDashTime < maxDashTime)
        {
            xMovement *= dashSpeed;
            currentDashTime += dashStoppingSpeed;
        }
        
        position.x += xMovement;
        position.y += movement.y * Time.deltaTime * jumpHeight;
        transform.position = position;
    }

    public void GetPlayerMovement(InputAction.CallbackContext context)
    {
        movement.x = context.ReadValue<float>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            movement.y = 0;
        }
    }


}