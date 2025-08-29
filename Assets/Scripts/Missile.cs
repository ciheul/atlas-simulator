using System;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class Missile : MonoBehaviour
{
    public MissileSO missileSO;
    public UIData uiData;
    private GameObject jet;
    public GameObject hitMissText;
    long timer;
    long now;

    private void Awake()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
        now = timer;

        jet = Resources.Load<AtlasSO>("AtlasSO").lockedOnJet;
        hitMissText = GameObject.Find("HitIndicator");
        Instantiate(missileSO.smokeSFX, transform);
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    void FixedUpdate()
    {
        now = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();

        float eulerAngleX = 0f;
        float eulerAngleY = 0f;

        if (jet != null)
        {
            if (!jet.IsDestroyed()){
                Homing(ref eulerAngleX, ref eulerAngleY);
      
            }
        }
        FormwardMovement(eulerAngleY, eulerAngleX);

        // flight distance limit
        if ((now - timer) * missileSO.speed >= 5000)
        {
            Destroy(gameObject);
            hitMissText.GetComponent<TMP_Text>().text = uiData.jetMiss;
        }
    }

    private void Homing(ref float eulerAngleX, ref float eulerAngleY)
    {
        // homing script
        RaycastHit hit;
        Vector3 targetDirection = jet.transform.position - transform.position;

        Vector3 horizontalTargetDirection = new Vector3(targetDirection.x, 0f, targetDirection.z);
        Vector3 verticalTargetDirection = new Vector3(0f, targetDirection.y, targetDirection.z);

        Vector3 verticalTargetDirectionRay = new Vector3(targetDirection.x, targetDirection.y, targetDirection.z);

        Debug.DrawRay(transform.position, targetDirection, Color.red, 0.01f); // testing
        Debug.DrawRay(transform.position, horizontalTargetDirection, Color.blue, 0.01f); // testing
        Debug.DrawRay(transform.position, verticalTargetDirectionRay, Color.green, 0.01f); // testing

        if (Physics.Raycast(transform.position, targetDirection, out hit, 300f))
        {
            if (hit.collider.CompareTag("jet"))
            {
                float targetAngleX = Vector3.SignedAngle(transform.forward, horizontalTargetDirection, Vector3.up); // left - right +
                float targetAngleY = Vector3.SignedAngle(transform.forward, verticalTargetDirection, Vector3.right); // down - up +
                float targetAngle = Vector3.SignedAngle(transform.forward, targetDirection, Vector3.forward);

                if (targetAngle <= missileSO.seekerFOV)
                {
                    if (targetAngleX > 0)
                    {
                        eulerAngleX = Mathf.Max(missileSO.maxTurnSpeed, targetAngleX * missileSO.turnSpeedMultiplier) * Time.fixedDeltaTime;
                    }
                    if (targetAngleX < 0)
                    {
                        eulerAngleX = Mathf.Min(-missileSO.maxTurnSpeed, targetAngleX * missileSO.turnSpeedMultiplier) * Time.fixedDeltaTime;
                    }

                    if (targetAngleY > 0)
                    {
                        eulerAngleY = Mathf.Max(missileSO.maxTurnSpeed, targetAngleY * missileSO.turnSpeedMultiplier) * Time.fixedDeltaTime;
                    }
                    if (targetAngleY < 0)
                    {
                        eulerAngleY = Mathf.Min(-missileSO.maxTurnSpeed, targetAngleY * missileSO.turnSpeedMultiplier) * Time.fixedDeltaTime;
                    }
                }
            }


        }

        /// turning buat homing
        if (jet.transform.position.z < transform.position.z)
        {
            eulerAngleY = -eulerAngleY;
        }
        transform.Rotate(new Vector3(eulerAngleY, eulerAngleX));
    }

    private void FormwardMovement(float eulerAngleY, float eulerAngleX)
    {
        // forward movement
        transform.Translate(missileSO.speed * Time.deltaTime * Vector3.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject explosion = Instantiate(missileSO.explosionVFX, transform.position, transform.rotation);
        Destroy(explosion, 1);
        Destroy(gameObject);

        if (other.CompareTag("jet"))
        {
            hitMissText.GetComponent<TMP_Text>().text = uiData.jetHit;
        }
        else
        {
            hitMissText.GetComponent<TMP_Text>().text = uiData.jetMiss;
        }
    }
}
