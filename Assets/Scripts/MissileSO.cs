using UnityEngine;

[CreateAssetMenu(fileName = "MissileSO", menuName = "Scriptable Objects/MissileSO")]
public class MissileSO : ScriptableObject
{
    public float speed = 70.0f;
    public float seekerFOV = 20f;
    public float turnSpeedMultiplier = 3f;
    public float maxTurnSpeed = 2f;
    public GameObject explosionVFX;
}
