using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float moveDistance;
    float distance = 0;
    float currentMoveDistance = 0;
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
    void Update()
    {

        // --- Patrol Movement ---
        if (huntState) 
        {
            ChasePlayer();
            return;
        }

        Patrol();
    }

    void Patrol()
    {
        currentMoveDistance = currentMoveDistance + 1 * Time.deltaTime * multiplier;
        UnityEngine.Vector2 position =  transform.position;
        position += moveVector * speed * direction * Time.deltaTime;
        transform.position = position;
        
        if (moveDistance <= currentMoveDistance) 
        {
            direction = direction * negative;
            currentMoveDistance = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // --- Wall Detection ---
        //FÜr einzelne Objekte gut
        // collision.attachedRigidbody.gameObject.tag != "Player";
        if (collision != null) collided = true;
        else collided = false;

        if (collision.gameObject.TryGetComponent<Health>(out Health health) && collision.CompareTag("Player")) { health.Damage(155); }
    }
    public void TurnAround()
    {
        // --- Das für Wall Turning ---
        if (collided)
        {
            direction = direction * negative;
            currentMoveDistance = moveDistance - currentMoveDistance;
        }
    }
    public void ChasePlayer()
    {
        // --- Der Enemy versucht zum Spieler zu gelangen kommt aber nicht durch Wände oder über Blöcke wäre auch gut wenn er dafür nicht
        // fallen müsste hat nämlich keine Gravity bisher ---
        if (playerLocation == null) return;

        huntState = true;
        UnityEngine.Vector3 playerDirection = playerLocation.position - transform.position;
        playerDirection.y = 0;
        playerDirection = playerDirection.normalized;

        transform.position += playerDirection * speed * Time.deltaTime  /2;

        aggroCounter += Time.deltaTime;
        if (aggroCounter >= aggroTime)
        {
            huntState = false;
            aggroCounter = 0;
        }
    }

    public void Die()
    {
        Destroy(gameObject);            
    }

    
}
