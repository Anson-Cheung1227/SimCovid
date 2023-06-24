using System;
using System.Collections.Generic;
using SimCovid.Core;
using SimCovid.Core.GameManagement;
using SimCovidAPI.Infection;
using SimCovidAPI.Locations.Policies;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SimCovid.UI
{
    /// <summary>
    /// Handles all UI elements
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        #region TimeUI
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private TextMeshProUGUI _dateText;
        #endregion TimeUI
        #region StateUI
        //[SerializeField] private DataManager.Instance DataManager.Instance.Instance; 
        [SerializeField] private GameObject _stateDetailUIPanel;
        [SerializeField] private TextMeshProUGUI _selectedStateNameText;
        [SerializeField] private TextMeshProUGUI _selectedStatePopulationText;
        [SerializeField] private TextMeshProUGUI _selectedStateInfectionsText;
        [SerializeField] private TextMeshProUGUI _selectedStateInHospitalText;
        [SerializeField] private TextMeshProUGUI _selectedStateRecoveredText;
        [SerializeField] private TextMeshProUGUI _selectedStateDeceasedText;
        #endregion StateUI
        #region LockdownUI
        [SerializeField] private GameObject _lockdownPanel;
        [SerializeField] GameObject _localLockdownButton;
        [SerializeField] GameObject _interstateLockdownButton;
        [SerializeField] GameObject _globalLockdownButton;
        [SerializeField] private TextMeshProUGUI _localLockdownText;
        [SerializeField] private TextMeshProUGUI _interstateLockdownText;
        [SerializeField] private TextMeshProUGUI _globalLockdownText;
        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _inactiveColor;
        #endregion LockdownUI
        [SerializeField] private ObjectPooler _pooler;
        #region AllStatesDetailsUI
        [SerializeField] private GameObject _allStatesDetailsUIPanel;
        [SerializeField] private GameObject _stateColumn;
        [SerializeField] private GameObject _stateColumnParent;
        #endregion
        private void Start()
        {
            GameEventManager.Instance.OnUpdateUI += UpdateUI;
            GameEventManager.Instance.OnActiveModalWindow += OnActiveModalWindow;
            InstantiateStateColumn(_stateColumn, _stateColumnParent, 50);
        }
        private void UpdateUI(DataManager dataManager)
        {
            UpdateTimeUI(dataManager);
            SetActiveStateDetailPanel(dataManager.ActiveStateDetailsPanel);
            UpdateStateDetailUI(dataManager);
            if (_lockdownPanel.activeInHierarchy && dataManager.SelectedState != null) UpdateLockdownUI(dataManager);
            if (Input.GetMouseButton(0))
            {
                if (IsPointerOverNothingWhenClick())
                {
                    dataManager.SelectedState = null;
                }
            }
            if (_allStatesDetailsUIPanel.activeInHierarchy) GameEventManager.Instance.InvokeOnUpdateAllStateDetailsTable(dataManager.StateInfectionsTable);
        }
        private void OnActiveModalWindow(string header, Sprite image, string contentText, string buttonText)
        {
            GameObject modalWindow = _pooler.Pools[0].GetPooledObject();
            modalWindow.GetComponent<ModalWindowController>().SetContent(header, image, contentText, buttonText);
            modalWindow.SetActive(true);
        }
        private void UpdateTimeUI(DataManager dataManager)
        {
            _timeText.text = dataManager.GameDateTime.ToString("HH:mm");
            _dateText.text = dataManager.GameDateTime.ToString("yyyy.MM.dd");
        }
        private void SetActiveStateDetailPanel(bool active)
        {
            if (_stateDetailUIPanel.activeInHierarchy != active)
            {
                _stateDetailUIPanel.SetActive(active);
            }
        }
        private void UpdateStateDetailUI(DataManager dataManager)
        {
            if (dataManager.SelectedState == null)
            {
                _selectedStateNameText.text = String.Empty;
                _selectedStatePopulationText.text = String.Empty;
                _selectedStateInfectionsText.text = String.Empty;
                _selectedStateInHospitalText.text = String.Empty;
                _selectedStateRecoveredText.text = String.Empty;
                _selectedStateDeceasedText.text = String.Empty;
            }
            else
            {
                if (dataManager.SelectedState.InfectionManager == null) return;
                _selectedStateNameText.text = dataManager.SelectedState.Name;
                _selectedStatePopulationText.text = LongToString(dataManager.SelectedState.Population);
                _selectedStateInfectionsText.text = LongToString(dataManager.SelectedState.InfectionManager
                    .GetTotalISpreadableCount());
                _selectedStateInHospitalText.text = LongToString(dataManager.SelectedState.InfectionManager
                    .GetISpreadableDataHandler(InfectionStatus.InHospital).GetActualISpreadablesCount());
                _selectedStateRecoveredText.text = LongToString(dataManager.SelectedState.InfectionManager
                    .GetISpreadableDataHandler(InfectionStatus.Recovered).GetActualISpreadablesCount());
                _selectedStateDeceasedText.text = LongToString(dataManager.SelectedState.InfectionManager
                    .GetISpreadableDataHandler(InfectionStatus.Deceased).GetActualISpreadablesCount());
            }
        }
        private void UpdateLockdownUI(DataManager dataManager)
        {
            //TODO:
            /*
            boolToActiveText(_localLockdownText, dataManager.SelectedState.LocalLockdown);
            boolToActiveText(_interstateLockdownText, dataManager.SelectedState.InterstateLockdown);
            boolToActiveText(_globalLockdownText, dataManager.SelectedState.GlobalLockdown);
            */
        }
        /*  
        This function takes a boolean, if it's true, set the text to active, and the corresponding color, 
        if it's false, set the text to inactive
    */
        private void boolToActiveText(TextMeshProUGUI textMeshProUGUI, bool active)
        {
            if (active)
            {
                textMeshProUGUI.text = "Active";
                textMeshProUGUI.color = _activeColor;
            }
            else
            {
                textMeshProUGUI.text = "Inactive";
                textMeshProUGUI.color = _inactiveColor;
            }
        }
        private bool IsPointerOverNothingWhenClick()
        {
            //Raycastall requires a pointerEventData, to record the pointer data
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            //Set the poimter positiom
            pointerEventData.position = Input.mousePosition;
            //Create a ;ist for results
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            //Perform the raycast, the result is returned to raycastResults
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);
            return raycastResults.Count == 0;
        }
        public void OnLockdownButtonClick()
        {
            //Hide the UI by setting it to inactive
            _lockdownPanel.SetActive(!_lockdownPanel.activeInHierarchy);
        }
        public void OnLockdownUpdateButtonClickUnityEvent(GameObject button)
        {
            OnLockdownUpdateButtonClick(GameManager.Instance.DataManager, button);
        }
        public void OnLockdownUpdateButtonClick(DataManager dataManager, GameObject button)
        {
            if (button == _localLockdownButton)
                dataManager.SelectedState.PolicyManager.GetPolicy(PolicyDefaultTypes.Lockdown)
                    .SetActive(!dataManager.SelectedState.PolicyManager.GetPolicy(PolicyDefaultTypes.Lockdown).Active);
            //TODO:
            /*
            if (button == _interstateLockdownButton) dataManager.SelectedState.InterstateLockdown = !dataManager.SelectedState.InterstateLockdown;
            if (button == _globalLockdownButton)
            {
                dataManager.SelectedState.GlobalLockdown = !dataManager.SelectedState.GlobalLockdown;
                dataManager.SelectedState.DailyIncomingPeople = 0;
            }
            */
        }
        public void OnStateDetailExitClick(DataManager dataManager)
        {
            dataManager.ActiveStateDetailsPanel = false;
        }
        public static string LongToString(long number)
        {
            /*
            1,000,000,000,000 = 1 Trillion (T)
            1,000,000,000 = 1 Billion (B)
            1,000,000 = 1 Million (M)
            1,000 = 1 thousand (K)
        */
            if (number >= 1000000000000 || number <= -100000000000) return $"{Math.Round((decimal)number / 1000000000000, decimals: 3)}T";
            else if (number >= 1000000000 || number <= -1000000000) return $"{Math.Round((decimal)number / 1000000000, decimals: 3)}B";
            else if (number >= 1000000 || number <= -100000) return $"{Math.Round((decimal)number / 1000000, decimals: 3)}M";
            else if (number >= 1000 || number <= -1000) return $"{Math.Round((decimal)number / 1000, decimals: 3)}K";
            else return $"{Math.Round(number, decimals: 3)}";
        }
        public void OnMandatoryMaskButtonClick(DataManager dataManager)
        {
            //TODO:
            //dataManager.SelectedState.MandatoryMask = !dataManager.SelectedState.MandatoryMask;
        }
        public void OnModalWindowButtonClick(GameObject modalWindow)
        {
            modalWindow.SetActive(false);
        }
        public void OnAllStatesDetailsPanelButtonClick()
        {
            _allStatesDetailsUIPanel.SetActive(!_allStatesDetailsUIPanel.activeInHierarchy);
        }
        private void InstantiateStateColumn(GameObject column, GameObject parent, int amount)
        {
            GameObject target;
            for (int i = 0; i < amount; ++i)
            {
                target = Instantiate(column);
                target.transform.SetParent(parent.transform, false);
                target.GetComponent<AllStateDetailsTableTextController>().Id = i;
            }
        }
    }
}