using System;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    #region TimeUI
    private string _timeTextContent; 
    [SerializeField] private TimeController _timeControllerRef; 
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _dateText; 
    #endregion TimeUI
    #region StateUI
    [SerializeField] private DataManager _dataManager; 
    [SerializeField] private TextMeshProUGUI _selectedStateNameText;
    [SerializeField] private TextMeshProUGUI _selectedStatePopulationText;
    [SerializeField] private TextMeshProUGUI _selectedStateInfectionsText;
    [SerializeField] private TextMeshProUGUI _selectedStateInHospitalText;
    [SerializeField] private TextMeshProUGUI _selectedStateRecoveredText;  
    #endregion StateUI
    #region LockdownUI
    [SerializeField] private GameObject _lockdownPanel; 
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
        UpdateStateDetailUI();
        if (_lockdownPanel.activeInHierarchy) UpdateLockdownUI();
    }
    private void UpdateTimeUI()
    {
        _timeTextContent = string.Empty;
        if (_timeControllerRef.GameTime.Hour < 10) //two digits
        {
            _timeTextContent += $"0{(int)_timeControllerRef.GameTime.Hour}";
        }
        else
        {
            _timeTextContent += (int)_timeControllerRef.GameTime.Hour; 
        }
        _timeTextContent += ":";
        if (_timeControllerRef.GameTime.Minute < 10)
        {
            _timeTextContent += $"0{(int)_timeControllerRef.GameTime.Minute}";
        }
        else
        {
            _timeTextContent += (int)_timeControllerRef.GameTime.Minute; 
        }
        _timeText.text = _timeTextContent;
        //Date UI:
        _dateText.text = (string)_timeControllerRef.GameDate;
    }
    private void UpdateStateDetailUI()
    {
        _selectedStateNameText.text = _dataManager.SelectedState.Name;
        _selectedStatePopulationText.text = LongToString(_dataManager.SelectedState.Population);
        _selectedStateInfectionsText.text = LongToString(_dataManager.SelectedState.InfectionsLong);
        _selectedStateInHospitalText.text = LongToString(_dataManager.SelectedState.InHospitalLong);
        _selectedStateRecoveredText.text = LongToString(_dataManager.SelectedState.RecoveredLong);
    }
    private void UpdateLockdownUI()
    {
        boolToActiveText(_localLockdownText, _dataManager.SelectedState.LocalLockdown);
        boolToActiveText(_interstateLockdownText, _dataManager.SelectedState.InterstateLockdown);
        boolToActiveText(_globalLockdownText, _dataManager.SelectedState.GlobalLockdown);
    }
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
    public void OnLockdownButtonClick()
    {
        _lockdownPanel.SetActive(!_lockdownPanel.activeInHierarchy);
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
