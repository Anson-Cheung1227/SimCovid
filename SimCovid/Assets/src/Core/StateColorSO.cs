using UnityEngine;

namespace SimCovid.Core
{
    /// <summary>
    /// Color Settings for States, read-only
    /// </summary>
    [CreateAssetMenu(fileName = "New State Color", menuName = "Scriptable Objects/State Color")]
    public class StateColorSO : ScriptableObject
    {
        public Color OriginalColor;
        public Color HoveringColor;
        public Color SelectedColor;
    }
}