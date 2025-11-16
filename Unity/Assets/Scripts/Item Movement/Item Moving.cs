using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemMoving : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float moveDistance;
    float distance = 0;
    float currentMoveDistance = 2f;
    int multiplier = 10;
    UnityEngine.Vector2 moveVector = new UnityEngine.Vector2(1, 0);
    int negative = -1;
    int direction = 1;
    bool collided;
    Transform playerLocation;
    public bool huntState;
    [SerializeField] int aggroTime;
    float aggroCounter = 0f;



    void Start()
    {
        float currentMoveDistance = distance;

        playerLocation = GameObject.FindWithTag("Player").transform;
        huntState = false;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        // --- Wall Detection ---
        // FÜr einzelne Objekte gut
        // collision.attachedRigidbody.gameObject.tag != "Player";
        if (collision != null) collided = true;
        else collided = false;

        if (collision.gameObject.TryGetComponent<Health>(out Health health) && collision.CompareTag("Player")) { health.Heal(1); }
    }
    public void ChasePlayer()
    {
        // --- Der Enemy versucht zum Spieler zu gelangen kommt aber nicht durch Wände oder über Blöcke wäre auch gut wenn er dafür nicht
        // fallen müsste hat nämlich keine Gravity bisher ---
        if (playerLocation == null) return;

        huntState = true;
        UnityEngine.Vector3 playerDirection = playerLocation.position - transform.position;
        // playerDirection.y = 0;
        playerDirection = playerDirection.normalized;

        transform.position += playerDirection * speed * Time.deltaTime  ;

        aggroCounter += Time.deltaTime;
        if (aggroCounter >= aggroTime)
        {
            huntState = false;
            aggroCounter = 1f;
        }
    }

    public void Die()
    {
        Destroy(gameObject);

        // if (collision.gameObject.TryGetComponent<Health>(out Health health))  { health.Heal(1); }
    }

    
}
