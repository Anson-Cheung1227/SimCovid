using System;
using UnityEngine;
using UnityEngine.Events;

public class TimeController : MonoBehaviour
{
    [SerializeField] private float[] _changeMinutes = new float[5];
    [SerializeField] public UnityEvent GenerateInfections; 
    [SerializeField] public UnityEvent GenerateInHospital;
    [SerializeField] public UnityEvent GenerateRecovery;
    [SerializeField] public UnityEvent GenerateDeath;
    [SerializeField] public UnityEvent UpdateMorale;
    private DateTime _lastUpdateDateTime;
    //public Property to control the speed of time;
    public int GameSpeed { get; private set; }
    private void Start()
    {
       _lastUpdateDateTime = DataManager.Instance.GameDateTime;
    }

    private void Update()
    {
        //Setting game speed
        if (Input.GetKeyDown(KeyCode.Alpha0)) GameSpeed = 0; //Pause the time
        if (Input.GetKeyDown(KeyCode.Alpha1)) GameSpeed = 1; //1 second (in real life) = 1 hour (in game time)
        if (Input.GetKeyDown(KeyCode.Alpha2)) GameSpeed = 2; //1 second (in real life) = 2 hour (in game time)
        if (Input.GetKeyDown(KeyCode.Alpha3)) GameSpeed = 3; //1 second (in real life) = 4 hour (in game time)
        if (Input.GetKeyDown(KeyCode.Alpha4)) GameSpeed = 4; //1 second (in real life) = 8 hour (in game time)
        //Change the Game Speed;
        DataManager.Instance.GameDateTime = DataManager.Instance.GameDateTime.AddMinutes(_changeMinutes[GameSpeed] * Time.deltaTime);
        if (DataManager.Instance.GameDateTime.Date != _lastUpdateDateTime.Date)
        {
            GenerateInfections.Invoke();
            GenerateInHospital.Invoke();
            GenerateRecovery.Invoke();
            GenerateDeath.Invoke();
            UpdateMorale.Invoke();
            _lastUpdateDateTime = DataManager.Instance.GameDateTime;
        }
    }
}
