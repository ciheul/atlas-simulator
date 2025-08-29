using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    private bool _isPaused = false;
    public UnityEvent GamePaused;
    public UnityEvent GameResumed;

    AudioManager audioManager;

    private void Awake()
    {
        GameObject audioObject = GameObject.FindGameObjectWithTag("Audio");

        if (audioObject != null)
        {
            audioManager = audioObject.GetComponent<AudioManager>();
            if (audioManager == null)
            {
                Debug.LogWarning("AudioManager component not found on tagged object.");
            }
        }
        else
        {
            Debug.LogWarning("GameObject with tag 'Audio' not found.");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _isPaused = !_isPaused;

            if (_isPaused)
            {
                Pause();
                GamePaused.Invoke();
            } else
            {
                Resume();
                GameResumed.Invoke();
            }
        }
    }

    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("WelcomeScene");

        // ketika balik ke welcome scene kadang timeScale masih 0
        Time.timeScale = 1;
    }

    public void ClickButton()
    {
        audioManager.playSFX(audioManager.buttonClick);
    }

    public void ClickInput()
    {
        audioManager.playSFX(audioManager.inputClick);
    }
}
