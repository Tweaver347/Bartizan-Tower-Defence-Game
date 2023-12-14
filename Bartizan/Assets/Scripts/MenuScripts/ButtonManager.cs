using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonManager : MonoBehaviour
{
    /// <summary>
    /// Starts a New Game 
    /// </summary>
    public void NewGame()
    {
        SceneManager.LoadScene("Level 1");
        Debug.Log("Starting a New Game!");
    }

    /// <summary>
    /// Sets the current scene to the Level Select UI
    /// </summary>
    public void GoToLevelSelect()
    {
        SceneManager.LoadScene("Level Select");
        Debug.Log("Going to Level Select!");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GoToLevel(int levelID)
    {
        // load scene by index
        SceneManager.LoadScene(levelID);
    }

    /// <summary>
    /// Sets the current scene to the Credits Menu
    /// </summary>
    public void GoTOCredits()
    {
        SceneManager.LoadScene("Credits");
        Debug.Log("Going to the Credits!");
    }

    public void GoTOMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Debug.Log("Going to the Main Menu!");
    }


    /// <summary>
    /// Quits the application
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }

    public void disableObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}
