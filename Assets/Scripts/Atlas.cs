using UnityEngine;
using UnityEngine.UI;

public class Atlas : MonoBehaviour
{
    public AtlasSO atlasSO;
    public MissileSpawner missile1;
    public MissileSpawner missile2;
    private MissileSpawner activeLauncher;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AimingControl();

        FiringControl();
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

        // missile 1
        if (Input.GetButtonDown("Missile2"))
        {
            missile1.deactivate();
            missile2.activate();
            activeLauncher = missile2;
        }

        // fire missile
        if (activeLauncher != null && Input.GetButtonDown("Fire"))
        {
            activeLauncher.fireMissile();
            //Instantiate(atlasSO.missilePrefab, transform.position, transform.rotation);
        }
    }

    private void AimingControl()
    {
        float verticalAim = Input.GetAxis("Vertical") * atlasSO.rotationSpeed;
        transform.Rotate(Vector3.right * -verticalAim);

        float horizontalAim = Input.GetAxis("Horizontal") * atlasSO.rotationSpeed;
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
