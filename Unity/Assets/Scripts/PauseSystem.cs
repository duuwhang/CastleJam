using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseSystem : MonoBehaviour
{
    // Variables
    [SerializeField] private InputActionReference pause;
    [SerializeField] public int useless;

    [SerializeField] private GameObject pauseMenuUI;

    // --- Subscription (Attach the event handler) ---
    private void Awake()
    {
        // This is where you connect the 'OnPause' method to the 'performed' event
        pause.action.performed += OnPause;
        
    }

    // --- Unsubscription (Clean up the event handler) ---
    private void OnDestroy()
    {
        // Always unsubscribe to prevent memory leaks and unexpected behavior
    }

    // --- Event Handler (The method that runs when the button is pressed) ---
    public void OnPause(InputAction.CallbackContext context)
    {
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
    }
}