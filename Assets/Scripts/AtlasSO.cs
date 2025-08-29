using UnityEngine;

[CreateAssetMenu(fileName = "AtlasSO", menuName = "Scriptable Objects/AtlasSO")]
public class AtlasSO : ScriptableObject
{
    public float rotationSpeed = 0.2f;
    public GameObject lockedOnJet;

    private void Awake()
    {
        lockedOnJet = null;
    }
}
