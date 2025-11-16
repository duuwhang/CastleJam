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
    [SerializeField] private InputActionReference attack;
    [SerializeField] public float gravity;
    [SerializeField] public LayerMask RayCastLayer;
    Vector2 movement = new Vector2();
    public Rigidbody2D rigidBody;
    bool isGrounded;
    [SerializeField] public float maxDashTime;
    [SerializeField] public float dashSpeed;
    private float currentDashTime;
    private float width;
    private float offset;
    Animator animator;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponentInChildren<Animator>();
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();

        width = GetComponentInChildren<BoxCollider2D>().bounds.size.x;
        offset = width / 2 * 1.1f;

        jump.action.performed += OnJump;
        dash.action.performed += OnDash;

        if (TryGetComponent(out Health health))
        {
            health.Died += () =>
            {
                animator.SetTrigger("died");
                this.enabled = false;
            };
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
        animator.SetTrigger("dashStart");
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        gameObject.TryGetComponent<UniversalAttack>(out UniversalAttack uniAttack);
        uniAttack.DoAttack();
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
        if (!this.enabled) return;
        movement.x = context.ReadValue<float>();
        animator.SetBool("walking", movement.x != 0);
        spriteRenderer.flipX = movement.x < 0;
    }

    public float RayCastX(Vector2 position, float xMovement)
    {
        switch (xMovement)
        {
            case > 0.0f:
                Vector3 pos = new Vector3(position.x + offset, position.y, 0);
                Debug.DrawRay(pos, Vector2.right * xMovement, Color.yellow);
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
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.down, -yMovement, RayCastLayer);

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
