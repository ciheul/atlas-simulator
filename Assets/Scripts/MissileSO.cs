using UnityEngine;

[CreateAssetMenu(fileName = "MissileSO", menuName = "Scriptable Objects/MissileSO")]
public class MissileSO : ScriptableObject
{
    public float speed = 70.0f;
    public GameObject explosionVFX;
}
