using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeScene : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void LoadEasyScene()
    {
        // easy scene
        LoadSceneByDifficulty(0);
    }

    public void LoadMediumScene()
    {
        // medium scene
        LoadSceneByDifficulty(1);
    }

    public void LoadHardScene()
    {
        // hard scene
        LoadSceneByDifficulty(2);
    }

    private void LoadSceneByDifficulty(int difficulty)
    {
        // menyimpan key & value di cache
        PlayerPrefs.SetInt("difficulty", difficulty);
        SceneManager.LoadScene("GameScene");
    }

    public void ClickButton()
    {
        audioManager.playSFX(audioManager.buttonClick);
    }

    public void ClickInput()
    {
        audioManager.playSFX(audioManager.inputClick);
    }

    public void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
        Debug.Log("Player has quit the game");
    }
}
