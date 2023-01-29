using UnityEngine;

[CreateAssetMenu(fileName = "New State Color", menuName = "Scriptable Objects/State Color")]
public class StateColorSO : ScriptableObject
{
    public Color OriginalColor;
    public Color HoveringColor;
    public Color SelectedColor;    
}