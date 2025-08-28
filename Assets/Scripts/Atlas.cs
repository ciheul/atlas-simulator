using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Atlas : MonoBehaviour
{
    public AtlasSO atlasSO;
    public UIData uiData;
    public MissileSpawner missile1;
    public MissileSpawner missile2;
    private MissileSpawner activeLauncher;
    public bool lockOn;
    public GameObject lockOnStatus;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lockOn = false;
    }

    // Update is called once per frame
    void Update()
    {

        FiringControl();
    }

    private void FixedUpdate()
    {
        AimingControl();
    }

    public void LockOn()
    {
        if (!activeLauncher.argonActive)
        {
            return;
        }
        lockOn = true;
        lockOnStatus.GetComponent<TMP_Text>().text = uiData.onText;
    }

    private void FiringControl()
    {
        // missile 1
        if (Input.GetButtonDown("Missile1"))
        {
            missile1.activate();
            missile2.deactivate();
            activeLauncher = missile1;
        }

        // missile 2
        if (Input.GetButtonDown("Missile2"))
        {
            missile1.deactivate();
            missile2.activate();
            activeLauncher = missile2;
        }

        if(activeLauncher != null)
        {
            // argon
            if (Input.GetButtonDown("Argon"))
            {
                activeLauncher.activateArgon();
            }

            //lock
            // argon
            if (Input.GetButtonDown("Lock"))
            {
                LockOn();
            }

            // fire missile
            if (activeLauncher.isReadyToFire() && Input.GetButtonDown("Fire"))
            {
                activeLauncher.fireMissile();
            }
        }
    }

    private void AimingControl()
    {
        float verticalAim = Input.GetAxis("Vertical") * atlasSO.rotationSpeed * Time.fixedDeltaTime;
        transform.Rotate(Vector3.right * -verticalAim);

        float horizontalAim = Input.GetAxis("Horizontal") * atlasSO.rotationSpeed * Time.fixedDeltaTime;
        transform.Rotate(Vector3.up * horizontalAim, Space.World);

        //zoom
        float zoom = Input.GetAxis("Zoom");
        switch (zoom)
        {
            case 1:
                Camera.main.fieldOfView = atlasSO.zoomInFOV;
                break;
            case -1:
                Camera.main.fieldOfView = atlasSO.zoomOutFOV;
                break;
        }
    }
}
