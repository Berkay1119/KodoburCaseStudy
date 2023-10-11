using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartCanvas : MonoBehaviour
{
    [SerializeField] private GameObject failGameObject;
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject backgroundCanvas;

    private void OnEnable()
    {
        EventManager.PlayerDied += Show;
    }

    private void OnDisable()
    {
        EventManager.PlayerDied -= Show;
    }

    private void Show()
    {
        backgroundCanvas.SetActive(true);
        failGameObject.SetActive(true);
        button.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
