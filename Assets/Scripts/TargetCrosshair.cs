using UnityEngine;

public class TargetCrosshair : MonoBehaviour
{
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
    }
}
