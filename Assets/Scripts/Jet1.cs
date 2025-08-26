using UnityEngine;

public class Jet1 : MonoBehaviour
{
    public JetSO jetSO;

    // center point adalah Atlas sebagai target serangan jet
    // khususnya ketika terbang melingkari target
    public Transform centerPoint;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // memastikan scene tidak freezed
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody>();
        rb.maxLinearVelocity = jetSO.maxSpeed;
        rb.linearVelocity = new Vector3(-jetSO.initialSpeed, 0f, 0f); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        UpdateCircularMovement();
    }

    void UpdateCircularMovement()
    {
        rb.AddRelativeForce(new Vector3(0f, 0f, jetSO.initialSpeed) * jetSO.acceleration, ForceMode.Force);
        //rb.AddRelativeForce(transform.up * 9.81f, ForceMode.Force);

        float pith = jetSO.pitchSpeed * Time.fixedDeltaTime;
        Quaternion rotation = Quaternion.Euler(pith, 0f, 0f);
        rb.MoveRotation(rb.rotation * rotation);

        //Vector3 movement = 100f * Time.fixedDeltaTime * transform.forward;
        //rb.linearVelocity = new Vector3(-10f,0f,0f);


    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        //Destroy(other.gameObject);
        Debug.Log("collision occured");
    }
}
