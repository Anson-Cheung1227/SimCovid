using System;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    private string _timeTextContent; 
    [SerializeField] private TimeController _timeControllerRef; 
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _dateText; 
    [SerializeField] private DataManager _dataManager; 
    [SerializeField] private TextMeshProUGUI _selectedStateName;
    [SerializeField] private TextMeshProUGUI _selectedStatePopulation;
    [SerializeField] private TextMeshProUGUI _selectedStateInfections;
    [SerializeField] private TextMeshProUGUI _selectedStateInHospital;
    [SerializeField] private TextMeshProUGUI _selectedStateRecovered;  
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        #region TimeUI
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
        #endregion TimeUI
        #region StateDetailsUI
        _selectedStateName.text = _dataManager.SelectedState.Name;
        _selectedStatePopulation.text = LongToString(_dataManager.SelectedState.Population);
        _selectedStateInfections.text = LongToString(_dataManager.SelectedState.InfectionsLong);
        _selectedStateInHospital.text = LongToString(_dataManager.SelectedState.InHospitalLong);
        _selectedStateRecovered.text = LongToString(_dataManager.SelectedState.RecoveredLong);
        #endregion StateDetailsUI
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
