using UnityEngine;

[CreateAssetMenu(fileName = "UIData", menuName = "Scriptable Objects/UIData")]
public class UIData : ScriptableObject
{
    public readonly string missileLoadedText = "Loaded";
    public readonly string missileEmptyText = "Empty";
    public readonly string onText = "ON";
    public readonly string offText = "OFF";
    public readonly string jetHit = "Hit";
    public readonly string jetMiss = "Miss";
}
