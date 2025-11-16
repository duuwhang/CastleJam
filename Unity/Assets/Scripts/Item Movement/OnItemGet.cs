using UnityEngine;
using UnityEngine.Events;

public class OnItemGet : MonoBehaviour
{
    public UnityEvent ItemNearPlayer;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) {ItemNearPlayer?.Invoke();}
    }
}
