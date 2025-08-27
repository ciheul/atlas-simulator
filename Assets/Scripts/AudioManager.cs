using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource sfxSource;

    public AudioClip buttonClick;
    public AudioClip inputClick;

    public void playSFX(AudioClip clip)
    {
        bool sfxEnabled = PlayerPrefs.GetInt("sfx", 1) == 1;
        
        if (sfxEnabled)
        {
            sfxSource.PlayOneShot(clip);
        } else
        {
            Debug.Log("sfx disabled");
        }

    }
}
