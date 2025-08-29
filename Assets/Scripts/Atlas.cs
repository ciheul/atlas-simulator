using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Atlas : MonoBehaviour
{
    public AtlasSO atlasSO;
    public MissileSO missileSO;
    public UIData uiData;
    public MissileSpawner missile1;
    public MissileSpawner missile2;
    private MissileSpawner activeLauncher;
    public bool lockOn;
    public GameObject lockOnStatus;
    public GameObject hitMissText;

    // zoom settings
    private int currentZoomLevel = 0;
    private int minZoomLevel = 0;
    private int maxZoomLevel = 2;
    private float fieldOfViewValue = 20;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lockOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleZoom();
        FiringControl();

        //DEBUG
        if (atlasSO.lockedOnJet != null)
        {
            if (!atlasSO.lockedOnJet.IsDestroyed())
            {
                Vector3 targetDirection = atlasSO.lockedOnJet.transform.position - transform.position;
                RaycastHit hit;

                Debug.DrawRay(transform.position, targetDirection, Color.red, 0.01f); // testing
            }
        }
    }

    private void FixedUpdate()
    {
        AimingControl();
    }

    private void FiringControl()
    {
        // missile 1
        if (Input.GetButtonDown("Missile1"))
        {
            missile1.activate();
            missile2.deactivate();
            activeLauncher = missile1;
            deactivateLockOn();
            hitMissText.GetComponent<TMP_Text>().text = null;
        }

        // missile 2
        if (Input.GetButtonDown("Missile2"))
        {
            missile1.deactivate();
            missile2.activate();
            activeLauncher = missile2;
            deactivateLockOn();
            hitMissText.GetComponent<TMP_Text>().text = null;
        }

        if(activeLauncher != null)
        {
            // argon
            if (Input.GetButtonDown("Argon"))
            {
                activeLauncher.activateArgon();
                print($"activeLauncher:{activeLauncher.name}");
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
    void LockOn()
    {
        if (!activeLauncher.argonActive)
        {
            return;
        }

        GameObject[] jetList = GameObject.FindGameObjectsWithTag("jet");
        List<GameObject> targetJetList = new List<GameObject>();
        List<float> targetAngleList = new List<float>();

        for (int x=0; x<jetList.Length; x++)
        {
            Vector3 targetDirection = jetList[x].transform.position - transform.position;
            RaycastHit hit;

            Debug.DrawRay(transform.position, targetDirection, Color.red, 0.01f); // testing

            if (Physics.Raycast(transform.position, targetDirection, out hit, missileSO.maxDistance))
            {
                if (hit.collider.CompareTag("jet"))
                {
                    float targetAngle = Vector3.SignedAngle(transform.forward, targetDirection, Vector3.forward);

                    if (targetAngle <= missileSO.seekerFOV)
                    {
                        targetJetList.Add(jetList[x]);
                        targetAngleList.Add(Mathf.Abs(targetAngle));
                    }
                }
            }
        }

        int closestToCrosshairIndex = -1;
        if (targetJetList.Count > 0)
        {
            float closestToCrosshair = Mathf.Min(targetAngleList.ToArray());
            closestToCrosshairIndex = targetAngleList.IndexOf(closestToCrosshair);
            atlasSO.lockedOnJet = jetList[closestToCrosshairIndex];

            lockOn = true;
            lockOnStatus.GetComponent<TMP_Text>().text = uiData.onText;

            activeLauncher.StartArgonCountdown();
        }
    }

    void deactivateLockOn()
    {
        lockOn = false;
        lockOnStatus.GetComponent<TMP_Text>().text = uiData.offText;
    }

    private void AimingControl()
    {
        float verticalAim = Input.GetAxis("Vertical") * atlasSO.rotationSpeed * Time.fixedDeltaTime;
        transform.Rotate(Vector3.right * -verticalAim);

        float horizontalAim = Input.GetAxis("Horizontal") * atlasSO.rotationSpeed * Time.fixedDeltaTime;
        transform.Rotate(Vector3.up * horizontalAim, Space.World);
    }

    private void HandleZoom()
    {
        // zoom in
        if (Input.GetButtonDown("ZoomIn") && currentZoomLevel < maxZoomLevel)
        {
            print("zoom in");
            Camera.main.fieldOfView -= fieldOfViewValue;
            currentZoomLevel += 1;
        }
        // zoom out
        else if (Input.GetButtonDown("ZoomOut") && currentZoomLevel > minZoomLevel)
        {
            print("zoom out");
            Camera.main.fieldOfView += fieldOfViewValue;
            currentZoomLevel -= 1;
        }

    }
}
