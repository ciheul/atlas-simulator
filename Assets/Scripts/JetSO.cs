using UnityEngine;

[CreateAssetMenu(fileName = "JetSO", menuName = "Scriptable Objects/JetSO")]
public class JetSO : ScriptableObject
{
    public float maxSpeed = 50f;
    public float initialSpeed = 50f;
    public float acceleration = 9.81f;
    public float pitchSpeed = -5f;
}
