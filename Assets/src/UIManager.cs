using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    #region TimeUI
    private string _timeTextContent; 
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
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateTimeUI();
        SetActiveStateDetailPanel(DataManager.Instance.ActiveStateDetailsPanel);
        UpdateStateDetailUI();
        if (_lockdownPanel.activeInHierarchy && DataManager.Instance.SelectedState != null) UpdateLockdownUI();
        if (Input.GetMouseButton(0))
        {
            if (IsPointerOverNothingWhenClick())
            {
                DataManager.Instance.SelectedState = null;
            }
        }
    }
    private void UpdateTimeUI()
    {
        _timeTextContent = string.Empty;
        if (DataManager.Instance.GameTime.Hour < 10) //two digits
        {
            _timeTextContent += $"0{(int)DataManager.Instance.GameTime.Hour}";
        }
        else
        {
            _timeTextContent += (int)DataManager.Instance.GameTime.Hour; 
        }
        _timeTextContent += ":";
        if (DataManager.Instance.GameTime.Minute < 10)
        {
            _timeTextContent += $"0{(int)DataManager.Instance.GameTime.Minute}";
        }
        else
        {
            _timeTextContent += (int)DataManager.Instance.GameTime.Minute; 
        }
        _timeText.text = _timeTextContent;
        //Date UI:
        _dateText.text = (string)DataManager.Instance.GameDate;
    }
    private void SetActiveStateDetailPanel(bool active)
    {
        if (_stateDetailUIPanel.activeInHierarchy != active)
        {
            _stateDetailUIPanel.SetActive(active);
        }
    }
    private void UpdateStateDetailUI()
    {
        if (DataManager.Instance.SelectedState == null)
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
            _selectedStateNameText.text = DataManager.Instance.SelectedState.Name;
            _selectedStatePopulationText.text = LongToString(DataManager.Instance.SelectedState.Population);
            _selectedStateInfectionsText.text = LongToString(DataManager.Instance.SelectedState.InfectionsLong);
            _selectedStateInHospitalText.text = LongToString(DataManager.Instance.SelectedState.InHospitalLong);
            _selectedStateRecoveredText.text = LongToString(DataManager.Instance.SelectedState.RecoveredLong);
            _selectedStateDeceasedText.text = LongToString(DataManager.Instance.SelectedState.DeceasedLong);
        }
    }
    private void UpdateLockdownUI()
    {
        boolToActiveText(_localLockdownText, DataManager.Instance.SelectedState.LocalLockdown);
        boolToActiveText(_interstateLockdownText, DataManager.Instance.SelectedState.InterstateLockdown);
        boolToActiveText(_globalLockdownText, DataManager.Instance.SelectedState.GlobalLockdown);
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
    public void OnLockdownUpdateButtonClick(GameObject button)
    {
        if (button == _localLockdownButton) DataManager.Instance.SelectedState.LocalLockdown = !DataManager.Instance.SelectedState.LocalLockdown;
        if (button == _interstateLockdownButton) DataManager.Instance.SelectedState.InterstateLockdown = !DataManager.Instance.SelectedState.InterstateLockdown;
        if (button == _globalLockdownButton) 
        {
            DataManager.Instance.SelectedState.GlobalLockdown = !DataManager.Instance.SelectedState.GlobalLockdown;
            DataManager.Instance.SelectedState.DailyIncomingPeople = 0;
        }
    }
    public void OnStateDetailExitClick()
    {
        DataManager.Instance.ActiveStateDetailsPanel = false;
    }
    private string LongToString(long number)
    {
        /*
            1,000,000,000,000 = 1 Trillion (T)
            1,000,000,000 = 1 Billion (B)
            1,000,000 = 1 Million (M)
            1,000 = 1 thousand (K)
        */
        if (number >= 1000000000000 || number <= -100000000000) return $"{Math.Round((decimal)number/1000000000000, decimals: 3)}T";
        else if (number >= 1000000000 || number <= -1000000000) return $"{Math.Round((decimal)number/1000000000, decimals: 3)}B";
        else if (number >= 1000000 || number <= -100000) return $"{Math.Round((decimal)number/1000000, decimals: 3)}M";
        else if (number >= 1000 || number <= -1000) return $"{Math.Round((decimal)number/1000, decimals: 3)}K";
        else return $"{Math.Round(number, decimals: 3)}";
    }
}