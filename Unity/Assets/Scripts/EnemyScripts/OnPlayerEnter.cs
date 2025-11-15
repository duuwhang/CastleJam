using UnityEngine;
using UnityEngine.Events;

public class AggroTrigger : MonoBehaviour
{
    public UnityEvent OnPlayerEnter;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) {OnPlayerEnter?.Invoke();}
    }
}
