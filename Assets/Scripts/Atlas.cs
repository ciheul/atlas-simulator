using UnityEngine;

public class Atlas : MonoBehaviour
{
    public GameObject missilePrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Instantiate(missilePrefab, transform.position, missilePrefab.transform.rotation);
        }
    }
}
