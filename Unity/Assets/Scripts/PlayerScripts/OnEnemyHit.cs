using UnityEngine;
using UnityEngine.Events;

public class OnEnemyHit : MonoBehaviour
{
    public UnityEvent OnEnemyEnter;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy")) {OnEnemyEnter?.Invoke();}
    }
}
