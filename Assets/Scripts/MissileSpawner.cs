using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissileSpawner : MonoBehaviour
{
    public UIData uiData;
    public GameObject missilePrefab;
    public GameObject missileButton;
    bool active;
    bool loaded;
    public bool argonActive;
    bool argonSpent;
    public Atlas atlas;
    long timer;
    long now;
    long argonTimeLimit;
    long argonCountdown;
    public GameObject missileLoadedStatus;
    public GameObject argonStatus;
    public GameObject argonTimer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        active = false;
        loaded = true;
        argonActive = false;
        argonSpent = false;
        argonTimeLimit = 45L;
        argonCountdown = argonTimeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        print($"argonActive:{argonActive}; lockOn:{atlas.lockOn}; active:{active}; loaded:{loaded}");
        if(argonActive && atlas.lockOn && active && loaded)
        {
            now = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
            argonCountdown = argonTimeLimit - (now - timer);
            if (argonCountdown <= 0) 
            {
                argonActive = false;
                argonSpent = true;
            }
            argonTimer.GetComponent<TMP_Text>().text = $"({argonCountdown})";
        }
    }

    public bool isReadyToFire()
    {
        return active && loaded && argonActive && atlas.lockOn && !argonSpent;
    }

    public void activate()
    {
        active = true;
        missileButton.GetComponent<Button>().interactable = false;

        if (loaded)
        {
            missileLoadedStatus.GetComponent<TMP_Text>().text = uiData.missileLoadedText;
        }
        else {
            missileLoadedStatus.GetComponent<TMP_Text>().text = uiData.missileEmptyText;
        }

        if (argonActive)
        {
            argonStatus.GetComponent<TMP_Text>().text = uiData.onText;
        }
        else
        {
            argonStatus.GetComponent<TMP_Text>().text = uiData.offText;
        }
    }
    public void deactivate()
    {
        active = false;
        missileButton.GetComponent<Button>().interactable = true;
        argonTimer.GetComponent<TMP_Text>().text = "";
    }

    public void activateArgon()
    {
        if (argonSpent)
        {
            return;
        }
        argonActive = true;
        argonStatus.GetComponent<TMP_Text>().text = uiData.onText;
        timer = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
        now = timer;
    }

    public void fireMissile()
    {
        Instantiate(missilePrefab, transform.position, transform.rotation);
        loaded = false;
        missileLoadedStatus.GetComponent<TMP_Text>().text = uiData.missileEmptyText;

        argonActive = false;
        argonStatus.GetComponent<TMP_Text>().text = uiData.offText;
    }
}
