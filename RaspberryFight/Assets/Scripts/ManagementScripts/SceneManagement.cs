using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// This script is used to load scenes and quit the game
public class SceneManagement : MonoBehaviour
{
   
   public Button buttonOnFocus;

   // Select the button on focus when the scene is loaded
   void Start() {
      buttonOnFocus.Select();
   }

   // Load a scene by name
   public void LoadScene(string SceneToLoad) {
      SceneManager.LoadSceneAsync(SceneToLoad);
   }
   
   // Quit the game
   public void QuitGame() {
      Application.Quit();
   }
}
