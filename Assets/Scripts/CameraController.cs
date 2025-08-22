using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera _camera;

    // zoom settings
    private int currentZoomLevel = 0;
    private int minZoomLevel = 0;
    private int maxZoomLevel = 2;
    private float fieldOfViewValue = 20;

    // camera rotation
    public float verticalInput;
    public float horizontalInput;
    private float rotationSpeed = 100f;
    private float xRotation = 0f;
    private float yRotation = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ???
        //Cursor.lockState = CursorLockMode.Locked;

        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleZoom();
        //HandleRotation();
    }

    private void HandleZoom()
    {
        // zoom in
        if (Input.GetKeyDown(KeyCode.Equals) && currentZoomLevel < maxZoomLevel)
        {
            _camera.fieldOfView -= fieldOfViewValue;
            currentZoomLevel += 1;
        }
        // zoom out
        else if (Input.GetKeyDown(KeyCode.Minus) && currentZoomLevel > minZoomLevel)
        {
            _camera.fieldOfView += fieldOfViewValue;
            currentZoomLevel -= 1;
        }
        
    }

    private void HandleRotation()
    {
        verticalInput = Input.GetAxis("Vertical") * rotationSpeed * Time.deltaTime;
        horizontalInput = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        // putar kamera secara horizontal
        yRotation += horizontalInput;

        // putar kamera secara vertikal tapi hanya sampai 90 derajat.
        xRotation -= verticalInput;
        // kamera diputar secara vertikal dibatasi hanya sampai 90 derajat
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // rotate camera
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        //Camera.main.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
