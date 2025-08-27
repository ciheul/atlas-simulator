using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Settings : MonoBehaviour
{
    private bool sfxEnabled;
    private float pathRadius;
    public Toggle sfxToggle;
    public Slider pathRadiusSlider;
    [SerializeField] private TMP_Text pathRadiusText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitiateSFX();
        InitiatePathRadius();
    }

    private void InitiateSFX() // mengecek value untuk checkbox sfx
    {
        sfxEnabled = PlayerPrefs.GetInt("sfx", 1) == 1;
        sfxToggle.isOn = sfxEnabled;

        sfxToggle.onValueChanged.AddListener(OnSFXToggleChanged);
    }

    private void OnSFXToggleChanged(bool isOn)
    {
        PlayerPrefs.SetInt("sfx", isOn ? 1 : 0);
    }

    private void InitiatePathRadius()
    {
        pathRadius = PlayerPrefs.GetFloat("path-radius", 1.0f);
        Debug.Log("saved value: " + pathRadius);
        pathRadiusSlider.value = pathRadius;

        pathRadiusSlider.onValueChanged.AddListener(OnPathRadiusChanged);

        PathRadiusText(pathRadius);
    }

    private void OnPathRadiusChanged(float value)
    {
        PlayerPrefs.SetFloat("path-radius", value);
        Debug.Log("path radius: " + PlayerPrefs.GetFloat("path-radius", 1));

        PathRadiusText(value);
    }

    private void PathRadiusText(float value)
    {
        float roundedValue = value * 100;
        int intRoundedValue = Mathf.RoundToInt(roundedValue);

        pathRadiusText.text = intRoundedValue.ToString();
    }

}
