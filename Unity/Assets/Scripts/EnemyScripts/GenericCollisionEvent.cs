using UnityEngine;
using UnityEngine.Events;

public class CollisionDetect : MonoBehaviour
{
    public UnityEvent CollisionDetection;

    public void Start()
    {
        if (CollisionDetection == null)
            CollisionDetection = new UnityEvent();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        CollisionDetection.Invoke();
    }
}
