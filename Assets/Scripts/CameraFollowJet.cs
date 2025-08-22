using UnityEngine;

public class CameraFollowJet : MonoBehaviour
{
    // object yang diikuti oleh camera
    public GameObject jet;


    private Vector3 offset = new Vector3 (0, 5, -15);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // kamera selalu mengikuti jet ke manapun pergi
        transform.position = jet.transform.position + offset;
    }
}
