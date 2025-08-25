using UnityEngine;

[CreateAssetMenu(fileName = "JetSO", menuName = "Scriptable Objects/JetSO")]
public class JetSO : ScriptableObject
{
    public float maxSpeed = 50f;
    public float initialSpeed = 50f;
    public float acceleration = 9.81f;
    public float pitchSpeed = 8f;
    public float rollSPeed = 10f;
    //public float startHeading = 180f; // y rotation axis awalnya terbang arah kemana
}
