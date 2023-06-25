using System.Collections.Generic;
using SimCovidAPI.Locations;
using UnityEngine;

namespace SimCovid.Core
{
    /// <summary>
    /// Represents a state
    /// </summary>
    [System.Serializable]
    public sealed class State : LocationBase
    {
        public State(string name)
        {
            Name = name;
        }
        [field: SerializeField] public List<Airport> AirportList { get; set; } = new List<Airport>();
        #region Civilian
        [field: SerializeField] public float Morale { get; set; }
        #endregion Civilian
    }
}