using System.Collections.Generic;
using SimCovidAPI;
using SimCovidAPI.Infection;
using UnityEngine;

namespace SimCovid.Core
{
    /// <summary>
    /// Represents a state
    /// </summary>
    [System.Serializable]
    public class State : ILocation
    {
        #region Infections
        [field: SerializeField] public string Name { get; set; }
        [field: SerializeField] public long Population { get; set; }
        [field: SerializeField] public float LocalSpreadRate { get; set; }
        [field: SerializeField] public long DailyIncomingPeople { get; set; }
        [field: SerializeField] public ISpreadableManager InfectionManager { get; set; }
        [field: SerializeField] public List<Airport> AirportList { get; set; } = new List<Airport>();
        #endregion Infection
        #region Civilian
        [field: SerializeField] public float Morale { get; set; }
        #endregion Civilian
        #region Policies
        [field: SerializeField] public bool LocalLockdown { get; set; }
        [field: SerializeField] public bool InterstateLockdown { get; set; }
        [field: SerializeField] public bool GlobalLockdown { get; set; }
        [field: SerializeField] public bool MandatoryMask { get; set; }
        #endregion Policies
    }
}