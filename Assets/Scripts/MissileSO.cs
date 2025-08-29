using UnityEngine;

[CreateAssetMenu(fileName = "MissileSO", menuName = "Scriptable Objects/MissileSO")]
public class MissileSO : ScriptableObject
{
    public float speed = 300f;
    public float seekerFOV = 20f;
    public float turnSpeedMultiplier = 3f;
    public float maxTurnSpeed = 1.5f;
    public float maxDistance = 5000f;
    public float seekerMaxDistance = 1000f;
    public GameObject explosionVFX;
    public GameObject smokeSFX;
}
