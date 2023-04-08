using UnityEngine;

namespace SimCovid.Core
{
    /// <summary>
    /// Template for Airport class, should be read-only and should be loaded on Start
    /// </summary>
    [CreateAssetMenu(fileName = "New Airport", menuName = "Scriptable Objects/Airport")]
    public class AirportSO : ScriptableObject
    {
        [SerializeField] public Airport Airport;
        //Conversion
        public static explicit operator Airport(AirportSO airportSO)
        {
            return new Airport
            {
                Name = airportSO.Airport.Name,
                IATACode = airportSO.Airport.IATACode,
                YearlyPassengers = airportSO.Airport.YearlyPassengers,
                CityServed = airportSO.Airport.CityServed
            };
        }
    }
}
