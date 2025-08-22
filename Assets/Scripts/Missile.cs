using UnityEngine;

public class Missile : MonoBehaviour
{
    public MissileSO missileSO;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.Movement();
    }

    private void Movement()
    {
        transform.Translate(missileSO.speed * Time.deltaTime * Vector3.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Instantiate(missileSO.explosionVFX, transform);
    }
}
