using UnityEngine;

public class Atlas : MonoBehaviour
{
    public AtlasSO atlasSO;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Instantiate(atlasSO.missilePrefab, transform.position, transform.rotation);
        }

        this.AimingControl();
    }

    private void AimingControl()
    {
        //Vector3.right = pitch
        //Vector3.up = yaw
        //Vector3.forward = roll
        float verticalAim = Input.GetAxis("Vertical") * atlasSO.rotationSpeed;
        transform.Rotate(Vector3.right * -verticalAim);

        float horizontalAim = Input.GetAxis("Horizontal") * atlasSO.rotationSpeed;
        transform.Rotate(Vector3.up * horizontalAim, Space.World);
    }
}
