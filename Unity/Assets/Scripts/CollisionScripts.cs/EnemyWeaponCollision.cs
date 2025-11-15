using UnityEngine;
using UnityEngine.Events;

public class WeaponCollisionDetect : MonoBehaviour
{
    public UnityEvent WeaponDetection;

    public void Start()
    {
        if (WeaponDetection == null)
            WeaponDetection = new UnityEvent();
    }


    void OnTriggerEnter2D(Collider2D PlayerDetector)
    {
    if (PlayerDetector.CompareTag("EnemyWeapon"))
        {
            WeaponDetection.Invoke();
        }
    }
}
