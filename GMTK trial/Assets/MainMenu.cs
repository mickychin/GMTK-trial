using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
 // Replace "GameScene" with your game scene name
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
