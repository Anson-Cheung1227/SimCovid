using UnityEngine;

[CreateAssetMenu(fileName = "New State Color", menuName = "Scriptable Objects/StateColor")]
public class StateColorSO : ScriptableObject
{
    public Color OriginalColor;
    public Color HoveringColor;
    public Color SelectedColor;    
}