using UnityEngine;
using UnityEngine.SceneManagement; 

public class StartExitButtonController : MonoBehaviour
{
    // Function to start the game
    public void StartGame()
    {
        SceneManager.LoadScene("SingScene");
    }

    // Function to exit the game
    public void ExitGame()
    {
        Debug.Log("Game exited.");
        Application.Quit();
    }
}
