using UnityEngine;

public class CameraFollowMouse : MonoBehaviour
{
    private float rotationSpeed = 100f;
    private float xRotation = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        // Rotate the camera rig horizontally
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera itself vertically (or clamp rig's vertical rotation)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp vertical rotation
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
