using UnityEngine;

namespace SimCovid.Core
{
    /// <summary>
    /// Represents an Airport
    /// </summary>
    [System.Serializable]
    public class Airport
    {
        [field: SerializeField] public string Name { get; set; }
        [field: SerializeField] public string IATACode { get; set; }
        [field: SerializeField] public long YearlyPassengers { get; set; }
        [field: SerializeField] public string CityServed { get; set; }
    }
}