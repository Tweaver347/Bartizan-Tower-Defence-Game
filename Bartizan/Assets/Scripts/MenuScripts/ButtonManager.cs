using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonManager : MonoBehaviour
{
    /// <summary>
    /// Starts a New Game 
    /// </summary>
    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Starting a New Game!");
    }

    /// <summary>
    /// Sets the current scene to the Level Select UI
    /// </summary>
    public void GoToLevelSelect()
    {
        SceneManager.LoadScene("Levels");
        Debug.Log("Going to Level Select!");
    }

    /// <summary>
    /// Sets the current scene to the Credits Menu
    /// </summary>
    public void GoTOCredits()
    {
        SceneManager.LoadScene("Credits");
        Debug.Log("Going to the Credits!");
    }

    /// <summary>
    /// Quits the application
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }
}
