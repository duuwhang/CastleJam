using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FredMovement : MonoBehaviour
{
    enum MovementState {WALK, CLIMB};
    private MovementState movementState;
    [SerializeField] float speed;
    [SerializeField] float moveDistance;
    public Transform player;

    UnityEngine.Vector2 movement = new UnityEngine.Vector2();
    [SerializeField] float maxHeight;
    bool goodHeight;




    void Start()
    {
        movementState = MovementState.CLIMB;
    }
    void Update()
    {
        switch (movementState)
        {
            case MovementState.CLIMB:
                FollowPlayer();
                break;
            case MovementState.WALK:
                StayCloseToPlayer();
                break;
        }

        // --- Update Position ---
            Vector2 position = transform.position;
        // geh hoch auf die gewollte Höhe
        if (transform.position.y >= maxHeight)
        {
            
        } else
        {
            position.y += speed * Time.deltaTime;
        }
        // geh rüber zu da wo Player ist
        float targetX = player.position.x;
        position.x = Mathf.MoveTowards
        (
            position.x,
            targetX,
            speed * Time.deltaTime
        );
        
        transform.position = position;
    }

    public void FollowPlayer()
    {
        if(movementState == MovementState.WALK)
        {
            movementState = MovementState.CLIMB;
            //we go on Wall now
        }
        
    }

    public void StayCloseToPlayer()
    {
        
    }
    

    public void Die()
    {
        Destroy(gameObject);
    }


}
