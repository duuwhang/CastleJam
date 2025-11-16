using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] UIDocument uIDocument;
    private VisualElement root;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        root = uIDocument.rootVisualElement;

        var playBtn = root.Q<VisualElement>("Start");

        playBtn.RegisterCallback<ClickEvent>(OnClickEvent);

        var quitBtn = root.Q<VisualElement>("Quit");

        quitBtn.RegisterCallback<ClickEvent>(QuitEvent);
    }

    // Update is called once per frame
    private void OnClickEvent(ClickEvent evt)
    {
        Debug.Log("Play Button Clicked");
        SceneManager.LoadScene("Tutorial");
    }

    private void QuitEvent(ClickEvent evt)
    {
        Debug.Log("Quit Button Clicked");
        Application.Quit();
    }
}
