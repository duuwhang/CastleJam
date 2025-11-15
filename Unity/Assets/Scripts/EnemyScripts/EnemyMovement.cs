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
    bool huntState;
    [SerializeField] int aggroTime;
    int aggroCounter = 0;


    void Start()
    {
        float currentMoveDistance = distance;

        playerLocation = GameObject.FindWithTag("Player").transform;
        huntState = false;
    }
    void Update()
    {
        // --- Patrol Movement ---
        if (huntState) return;
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
        // --- Check if it is actually the Player because generalized one does not do that ---
        // --- Or we fuck it and make one specifically for the Player because I do not understand it ---
        Debug.Log("The Hunt is on!");
        if (playerLocation == null) return;
        huntState = true;
        while (aggroCounter < aggroTime)
        {
        Debug.Log("We chasing now");
        UnityEngine.Vector3 direction = playerLocation.position - transform.position;
        direction.y = 0;
        
        direction = direction.normalized;

        transform.position += direction * speed * Time.deltaTime;
        aggroCounter++;
        }
        huntState = false;
        aggroCounter = 0;
    }
    
}
