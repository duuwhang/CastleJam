using UnityEngine;
using UnityEngine.Events;

public class EnemyCollisions : MonoBehaviour
{
    public UnityEvent PlayerDetection;

    public void Start()
    {
        if (PlayerDetection == null)
            PlayerDetection = new UnityEvent();
    }


    void OnTriggerEnter2D(Collider2D PlayerDetector)
    {
    if (PlayerDetector.CompareTag("Player"))
        {
            PlayerDetection.Invoke();
        }

    

    }
}
