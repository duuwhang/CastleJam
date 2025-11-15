using UnityEngine;
using UnityEngine.Events;

public class EnemyTurnAround : MonoBehaviour
{
    public UnityEvent CollisionDetection;

    public void Start()
    {
        if (CollisionDetection == null)
            CollisionDetection = new UnityEvent();
    }


    void OnTriggerEnter(Collider other)
    {
        CollisionDetection.Invoke();
    }
}
