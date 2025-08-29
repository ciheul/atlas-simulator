using UnityEngine;

public class TargetCrosshair : MonoBehaviour
{
    public GameObject parentJet;
    public MissileSO missileSO;
    public AtlasSO atlasSO;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // adjust crosshair size dan rotation terhadap camera
        float currentDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float scaleFactor = currentDistance / 200f;
        transform.localScale = Vector3.one * scaleFactor;
        
        transform.LookAt(Camera.main.transform);

        // crosshair target leading
        if (atlasSO.lockedOnJet == parentJet)
        {
            float timeOnTarget = currentDistance / missileSO.speed;
            float leadingDistance = parentJet.GetComponent<Rigidbody>().linearVelocity.magnitude * timeOnTarget;
            Vector3 leading = parentJet.transform.position + parentJet.transform.forward * leadingDistance;
            transform.position = leading;
        }
        else
        {
            transform.position = parentJet.transform.position;
        }
    }
}
