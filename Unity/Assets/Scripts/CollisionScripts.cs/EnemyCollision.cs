using UnityEngine;
using UnityEngine.Events;

public class EnemyCollisionDetection : MonoBehaviour
{
    public UnityEvent EnemyDetection;

    public void Start()
    {
        if (EnemyDetection == null)
            EnemyDetection = new UnityEvent();
    }


    void OnTriggerEnter2D(Collider2D PlayerDetector)
    {
    if (PlayerDetector.CompareTag("Enemy"))
        {
            EnemyDetection.Invoke();
        }
    }
}
