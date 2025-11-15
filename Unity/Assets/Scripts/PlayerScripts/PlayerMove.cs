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
    [SerializeField] public float maxDashTime;
    [SerializeField] public float dashSpeed;
    public float dashStoppingSpeed = 0.1f;
    private float currentDashTime;
    private float width;
    private float offset;
    int health = 3;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        width = GetComponentInChildren<BoxCollider2D>().bounds.size.x;
        offset = width / 2 * 1.1f;

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
        movement.y -= gravity * Time.deltaTime;

        Vector2 position = transform.position;
        float xMovement = movement.x * Time.deltaTime * speed;
        if (currentDashTime < maxDashTime)
        {
            float dashDistance = dashSpeed * Time.deltaTime;
            if (xMovement > 0)
            {
                xMovement += dashDistance;
            }
            else if (xMovement < 0)
            {
                xMovement -= dashDistance;
            }
            currentDashTime += dashDistance;
        }
        xMovement = RayCastX(position, xMovement);
        position.x += xMovement;

        float yMovement = movement.y * Time.deltaTime * jumpHeight;
        yMovement = RayCastY(position, yMovement);
        position.y += yMovement;

        transform.position = position;
    }

    public void GetPlayerMovement(InputAction.CallbackContext context)
    {
        movement.x = context.ReadValue<float>();
    }

    public float RayCastX(Vector2 position, float xMovement)
    {
        switch (xMovement)
        {
            case > 0.0f:
                Vector3 pos = new Vector3(position.x + offset, position.y, 0);
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.right, xMovement);

                if (hit)
                {
                    float distance = hit.point.x - position.x - offset;
                    if (distance < xMovement)
                    {
                        return distance;
                    }
                }
                break;

            case < 0.0f:
                Vector3 pos1 = new Vector3(position.x - offset, position.y, 0);
                RaycastHit2D hit1 = Physics2D.Raycast(pos1, Vector2.left, -xMovement);

                if (hit1)
                {
                    float distance = position.x - hit1.point.x - offset;
                    if (distance < -xMovement)
                    {
                        return -distance;
                    }
                }
                break;
        }
        return xMovement;
    }

    public float RayCastY(Vector2 position, float yMovement)
    {
        if (yMovement < 0)
        {
            Vector3 pos = new Vector3(position.x, position.y - offset, 0);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.down, -yMovement);

            if (hit)
            {
                float distance = -hit.point.y - -position.y - offset;
                if (distance < -yMovement)
                {
                    isGrounded = true;
                    movement.y = 0;
                    return -distance;
                }
            }
        }
        return yMovement;
    }
}
