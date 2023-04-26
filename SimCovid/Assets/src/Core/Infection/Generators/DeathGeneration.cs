using System.Collections.Generic;
using SimCovidAPI;

namespace SimCovid.Core.Infection.Generators
{
    /// <summary>
    /// Handles Death Generation
    /// </summary>
    public class DeathGeneration : DeathGenerationBase
    {
        private DataManager _dataManager;
        public DeathGeneration(List<ILocation> locationList, DataManager dataManager)
        {
            Locations = locationList;
            _dataManager = dataManager;
        }

        public override void OnGenerate()
        {
            TargetDate = _dataManager.GameDateTime;
            base.OnGenerate();
        }
    }
}
