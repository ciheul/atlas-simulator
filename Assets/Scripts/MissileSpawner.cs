using UnityEngine;
using UnityEngine.UI;

public class MissileSpawner : MonoBehaviour
{
    public GameObject missilePrefab;
    public GameObject missileButton;
    private bool active;
    private bool loaded;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        active = false;
        loaded = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activate()
    {
        active = true;
        missileButton.GetComponent<Button>().interactable = false;
    }

    public void deactivate()
    {
        active = false;
        missileButton.GetComponent<Button>().interactable = true;
    }

    public void fireMissile()
    {
        if (active && loaded)
        {
            Instantiate(missilePrefab, transform.position, transform.rotation);
            loaded = false;
        }
    }
}
