using System.Collections.Generic;
using SimCovidAPI.Infection.Generators;
using SimCovidAPI.Locations;

namespace SimCovid.Core.Infection.Generators
{
    /// <summary>
    /// Handle the transfer of patients to hospital
    /// </summary>
    public class InHospitalGeneration : InHospitalGenerationBase
    {
        private DataManager _dataManager;
        public InHospitalGeneration(List<ILocation> locationList, DataManager dataManager)
        {
            _dataManager = dataManager;
            Locations = locationList;
        }
        public override void OnGenerate()
        {
            TargetDate = _dataManager.GameDateTime;
            base.OnGenerate();
        }
    }
}