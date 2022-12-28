using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    private string _timeTextContent; 
    [SerializeField] private TimeController _timeControllerRef; 
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _dateText; 
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        //Time UI:
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
}
