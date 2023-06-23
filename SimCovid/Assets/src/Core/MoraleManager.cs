using System.Collections.Generic;
using UnityEngine;

namespace SimCovid.Core
{
    /// <summary>
    /// Manages the morale of states
    /// </summary>
    public class MoraleManager : MonoBehaviour
    {
        [SerializeField] private List<StateController> _allStates;
        // Start is called before the first frame update
        void Start()
        {
            GameEventManager.Instance.OnUpdateMorale += UpdateMorale;
        }
        public void UpdateMorale()
        {
            foreach (StateController stateController in _allStates)
            {
                stateController.State.Morale = (1 - (float)stateController.State.InfectionManager.GetTotalISpreadableCount() /stateController.State.Population) * (float)100;
            }
        }
    }
}
