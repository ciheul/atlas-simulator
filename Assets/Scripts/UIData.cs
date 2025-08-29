using UnityEngine;

[CreateAssetMenu(fileName = "UIData", menuName = "Scriptable Objects/UIData")]
public class UIData : ScriptableObject
{
    public string missileLoadedText = "Loaded";
    public string missileEmptyText = "Empty";
    public string onText = "ON";
    public string offText = "OFF";
}
