using System.Collections.Generic;
using SimCovid.Core;
using TMPro;
using UnityEngine;

namespace SimCovid.UI
{
    /// <summary>
    /// Controller for AllStateDetailsPanel
    /// </summary>
    public class AllStateDetailsTableTextController : MonoBehaviour
    {
        public int Id;
        // References to Panel objects
        [SerializeField] private TextMeshProUGUI _stateNameText;
        [SerializeField] private TextMeshProUGUI _stateInfectionsText;
        [SerializeField] private TextMeshProUGUI _stateInHospitalText;
        [SerializeField] private TextMeshProUGUI _stateRecoveredText;
        [SerializeField] private TextMeshProUGUI _stateDeceasedText;
        // Start is called before the first frame update
        void Start()
        {
            //Subscribing Handler to Event
            GameEventManager.Instance.OnUpdateAllStatesDetailsTable += OnUpdateStateDetailsTable;
        }
        //Handler for OnUpdateAllStatesDetailsTable
        private void OnUpdateStateDetailsTable(List<State> stateList)
        {
            _stateNameText.text = stateList[Id].Name;
            _stateInfectionsText.text = UIManager.LongToString(stateList[Id].InfectionManager.GetActive().GetActualInfectionsCount());
            _stateInHospitalText.text = UIManager.LongToString(stateList[Id].InHospitalLong);
            _stateRecoveredText.text = UIManager.LongToString(stateList[Id].RecoveredLong);
            _stateDeceasedText.text = UIManager.LongToString(stateList[Id].DeceasedLong);
        }
    }
}
