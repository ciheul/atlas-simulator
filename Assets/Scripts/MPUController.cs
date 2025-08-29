using System.IO.Ports;
using UnityEngine;

public class MPUController : MonoBehaviour
{
    SerialPort serial = new SerialPort("COM3", 9600);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        serial.Open();
    }

    // Update is called once per frame
    void Update()
    {
        if (serial.IsOpen)
        {
            string data = serial.ReadLine();
            string[] values = data.Split(",");

            if (values.Length == 3)
            {
                float pitch = float.Parse(values[0]);
                float roll = float.Parse(values[1]);
                float yaw = float.Parse(values[2]);

                Quaternion targetRotation = Quaternion.Euler(pitch, roll, yaw);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
            }
        }
    }
}
