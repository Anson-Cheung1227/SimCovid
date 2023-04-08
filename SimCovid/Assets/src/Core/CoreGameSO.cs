using System.Collections.Generic;
using UnityEngine;

namespace SimCovid.Core
{
    /// <summary>
    /// Represents a Scenario, and all resources to load
    /// </summary>
    [CreateAssetMenu(fileName = "New Core Game", menuName = "Scriptable Objects/CoreGameSO")]
    public class CoreGameSO : ScriptableObject 
    {
        [SerializeField] public List<SceneEnum> SceneList;
    }
}