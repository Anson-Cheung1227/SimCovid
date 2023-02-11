using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeController : MonoBehaviour
{
    [SerializeField] private float[] _changeMinutes = new float[5];
    [SerializeField] public UnityEvent GenerateInfections; 
    [SerializeField] public UnityEvent GenerateInHospital;
    [SerializeField] public UnityEvent GenerateRecovery;
    [SerializeField] public UnityEvent UpdateMorale;
    //public Property to control the speed of time;
    public int GameSpeed { get; private set; }
    private void Start()
    {
       
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
        DataManager.Instance.GameTime += new TimeModule.Time(0, _changeMinutes[GameSpeed] * Time.deltaTime, 0);
        if (DataManager.Instance.GameTime.Hour >= 24)
        {
            GenerateInfections.Invoke();
            GenerateInHospital.Invoke();
            GenerateRecovery.Invoke();
            UpdateMorale.Invoke();
            //Generate Infections
            DataManager.Instance.GameTime.Hour = 0;
            DataManager.Instance.GameDate += new TimeModule.Date(0, 0, 1);
        }
    }
}
