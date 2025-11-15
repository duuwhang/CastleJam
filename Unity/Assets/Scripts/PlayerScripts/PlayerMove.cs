using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpHeight;
    [SerializeField] private InputActionReference jump;
    [SerializeField] private InputActionReference dash;
    [SerializeField] public float gravity;
    [SerializeField] public float distanceMod;
    Vector2 movement = new Vector2();
    public float floatHeight;     // Desired floating height.
    public float liftForce;       // Force to apply when lifting the rigidbody.
    public float damping;         // Force reduction proportional to speed (reduces bouncing).
    public Rigidbody2D rigidBody;
    bool isGrounded;
    public Vector3 moveDirection;
    public float maxDashTime = 1.0f;
    [SerializeField] public float dashSpeed = 5.0f;
    public float dashStoppingSpeed = 0.1f;
    private float currentDashTime;

    LayerMask layerMask;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        layerMask = LayerMask.GetMask("Wall");

        jump.action.performed += OnJump;
        dash.action.performed += OnDash;

        if (TryGetComponent(out Health health))
        {
            health.Died += () => this.enabled = false;
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            isGrounded = false;
            movement.y = context.ReadValue<float>();
        }
    }

    private void OnDash(InputAction.CallbackContext context)
    {
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

        position.y += movement.y * Time.deltaTime * jumpHeight;
        float width = GetComponentInChildren<BoxCollider2D>().bounds.size.x;
        Vector3 pos = new Vector3(transform.position.x + width * 1.1f / 2, 0, 0);

        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.right, 100);

        if (hit)
        {
            float distance = hit.point.x - transform.position.x - width / 2;
            if (distance < xMovement)
            {
                xMovement = distance;
            }
        }
        position.x += xMovement;

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
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;

        }
    }
}


