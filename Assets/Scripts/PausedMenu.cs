using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedMenu : MonoBehaviour
{
    public PauseScene pauseSceneReference;
    private string pauseSceneName = "PauseScene";

    //void Start()
    //{
    //    if (pauseSceneReference != null)
    //    {
    //        pauseSceneReference.isPaused = pauseSceneReference.isPaused;
    //    }
    //}

    public void Resume()
    {
        SceneManager.UnloadSceneAsync(pauseSceneName);
        Debug.Log(pauseSceneReference.isPaused);
        Time.timeScale = 1f;
        pauseSceneReference.isPaused = false;
        Debug.Log(pauseSceneReference.isPaused);
    }

    public void Restart()
    {
        // Reset time scale in case the game was paused
        Time.timeScale = 1f;

        // Reload the active scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

}
