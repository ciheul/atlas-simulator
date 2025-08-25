using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScene : MonoBehaviour
{
    public bool isPaused = false;
    private string pauseSceneName = "PauseScene";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene(pauseSceneName, LoadSceneMode.Additive);
        isPaused = true;
        Cursor.visible = true;
    }

    void ResumeGame()
    {
        SceneManager.UnloadSceneAsync(pauseSceneName);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = false;
    }
}