using UnityEngine;
using UnityEngine.Events;

public class PlayerWeaponCollisionDetect : MonoBehaviour
{
    public UnityEvent PlayerWeaponDetection;

    public void Start()
    {
        if (PlayerWeaponDetection == null)
            PlayerWeaponDetection = new UnityEvent();
    }


    void OnTriggerEnter2D(Collider2D PlayerDetector)
    {
    if (PlayerDetector.CompareTag("PlayerWeapon"))
        {
            PlayerWeaponDetection.Invoke();
        }
    }
}
