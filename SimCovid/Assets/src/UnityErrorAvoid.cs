using UnityEngine;
using UnityEngine.Rendering;

namespace SimCovid
{
    /// <summary>
    /// Avoid build errors
    /// </summary>
    public class UnityErrorAvoid : MonoBehaviour
    {
        private void Awake()
        {
            DebugManager.instance.enableRuntimeUI = false;
        }
    }
}
