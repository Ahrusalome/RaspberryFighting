using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : SceneManagement
{
    private GameObject pauseMenuUI;

    void Start()
    {
        pauseMenuUI = GameObject.Find("CanvasPause");
        buttonOnFocus = GameObject.Find("ResumeButton").GetComponent<Button>();
        buttonOnFocus.Select();
        pauseMenuUI.SetActive(false);
    }

    void OnEscape()
    {
        Debug.Log("Escaping game succeeded");
        if (Time.timeScale == 0f)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("Pausing game succeeded");
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Debug.Log("Resuming game succeeded");
    }
}