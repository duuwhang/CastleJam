using UnityEngine;
using UnityEngine.InputSystem;

public class PauseSystem : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private InputActionReference pause;
    public void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    private bool isPaused = false;

    private void OnEnable()
    {
        pause.action.performed += OnPause;
        pause.action.Enable();
        pauseMenuUI.SetActive(true);
    }

    private void OnDisable()
    {
        pause.action.performed -= OnPause;
        pause.action.Disable();
        pauseMenuUI.SetActive(false);
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        if (!isPaused)
        {
            Time.timeScale = 0f;
            isPaused = true;
            Debug.Log("PAUSE triggered");
        }
        else
        {
            Time.timeScale = 1f;
            isPaused = false;
            Debug.Log("UNPAUSE triggered");
        }
    }
}