using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    Vector2 movement = new Vector2();
    


    private void Update()
    {
        Vector2 position =  transform.position;
        position += movement * Time.deltaTime * speed;
        transform.position =  position;
    }

    public void GetPlayerMovement(InputAction.CallbackContext context)
    {
        movement.x = context.ReadValue<float>();
    }
}