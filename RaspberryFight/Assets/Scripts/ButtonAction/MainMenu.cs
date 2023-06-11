using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadSceneAsync("Selector");
        Debug.Log("Loading game succeeded");
    }

    public void QuitGame() {
        Application.Quit();
        Debug.Log("Quitting game succeeded");
    }

    public void TimeRunning() {
        Time.timeScale = 1f;
    }
    public void LoadMenu()
    {
        if (DatabaseManager.instance) {
        DatabaseManager.instance.SetNonPlaying();
        }
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("Menu");
        Debug.Log("Loading menu succeeded");
    }
}