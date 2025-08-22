using UnityEngine;

public class Jet : MonoBehaviour
{
    // center point adalah Atlas sebagai target serangan jet
    // khususnya ketika terbang melingkari target
    public Transform centerPoint;

    // radius adalah jarak dari titik tengah ke garis lingkaran
    // radian adalah satuan jarak dari sebuah titik ke titik lain di garis lingkaran
    // yang jika kedua titik ini ditarik ke tengah lingkaran akan membentuk sudut (derajat/degree).
    public float radius = 5f;

    // satuan derajat (degree)
    public float currentAngle = 0f;

    // kecepatan pesawat tempur
    public float speed = 10.0f;
    
    // ubah jika ingin mengendalikan pesawat tempur
    private bool isJetControlled = false;

    // pesawat naik atau turun
    public float verticalInput;
    public float verticalRotationSpeed = 20.0f;

    // pesawat berputar searah jarum jam atau berlawan arah jarum jam
    public float horizontalInput;
    public float horizontalRotationSpeed = 20.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isJetControlled)
        {
            HandleJet();
        }

        //transform.Translate(Vector3.forward * Time.deltaTime * speed);
        UpdateCircularMovement();
    }

    private void HandleJet()
    {
        // pesawat naik ke atas atau turun ke bawah berdasarkan keyboard/joystick
        verticalInput = Input.GetAxis("Vertical");
        transform.Rotate(Vector3.left * verticalRotationSpeed * Time.deltaTime * -verticalInput);

        // pesawat berputar searah jarum jam atau berlawanan arah jarum jam
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.forward * horizontalRotationSpeed * Time.deltaTime * -horizontalInput);
    }

    void UpdateCircularMovement()
    {
        // setiap detiknya jet terbang dari satu titik ke titik lain
        // akan membentuk sudut terhadap tengah lingkaran (atlas)
        currentAngle += speed * Time.deltaTime;

        // kemudian, sudut diubah menjadi radian
        float angleRad = currentAngle * Mathf.Deg2Rad;

        // Calculate new position relative to the center
        float x = centerPoint.position.x + Mathf.Cos(angleRad) * radius;
        float z = centerPoint.position.z + Mathf.Sin(angleRad) * radius; // Or 'y' for 2D

        // Apply the new position
        transform.position = new Vector3(x, 80, z); // Keep the y-position of the center
        //transform.position = new Vector3(x, transform.position.y, z); // Keep the y-position of the center

        
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Destroy(other.gameObject);
        Debug.Log("collision occured");
    }
}
