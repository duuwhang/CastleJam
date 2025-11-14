using System;
using System.Numerics;
using UnityEngine;

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


    void Start()
    {
        float currentMoveDistance = distance;
    }
    void Update()
    {

        currentMoveDistance = currentMoveDistance + 1 * Time.deltaTime * multiplier;

        UnityEngine.Vector2 position = transform.position;
        position += moveVector * speed * direction * Time.deltaTime;
        transform.position = position;

        Debug.Log(currentMoveDistance);
        if (moveDistance <= currentMoveDistance)
        {
            direction = direction * negative;
            currentMoveDistance = 0;
        }
    }
}
