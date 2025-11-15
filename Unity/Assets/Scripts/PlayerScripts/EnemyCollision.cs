using UnityEngine;
using UnityEngine.Events;

public class EnemyCollisionDetect : MonoBehaviour
{
    public UnityEvent PlayerDetection;

    public void Start()
    {
        if (PlayerDetection == null)
            PlayerDetection = new UnityEvent();
    }


    void OnTriggerEnter2D(Collider2D PlayerDetector)
    {
    if (PlayerDetector.CompareTag("Enemy"))
        {
            PlayerDetection.Invoke();
        }

    

    }
}
