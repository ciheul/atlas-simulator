using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeScene : MonoBehaviour
{
    public void LoadSceneByName(string sceneName)
    {
        // get scene menggunakan index yg ada di file -> build profiles -> scene list
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player has quit the game");
    }
}
