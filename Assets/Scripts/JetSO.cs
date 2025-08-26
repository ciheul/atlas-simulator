using UnityEngine;

[CreateAssetMenu(fileName = "JetSO", menuName = "Scriptable Objects/JetSO")]
public class JetSO : ScriptableObject
{
    public float maxSpeed = 50f;
    public float initialSpeed = 50f;
    public float acceleration = 9.81f;
    public float pitchSpeed = 0.5f;
    public float rollSpeed = 1f;
    //public float startHeading = 180f; // y rotation axis awalnya terbang arah kemana
}
