using UnityEngine;

namespace SimCovid.Core
{
    /// <summary>
    /// Constant Stats for game, should be loaded on Start
    /// </summary>
    [CreateAssetMenu(fileName = "New Global Stat", menuName = "Scriptable Objects/Global Stat")]
    public class GlobalStatsSO : ScriptableObject
    {
        public float RecoveryRate;
        public float DeathRate;
    }
}