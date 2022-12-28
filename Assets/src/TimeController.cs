using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeController : MonoBehaviour
{
    [SerializeField] private float[] _changeMinutes = new float[5];
    [SerializeField] public UnityEvent _generateInfections; 
    //public Property to control the speed of time;
    public int GameSpeed { get; private set; }
    public TimeModule.Date GameDate { get; private set; } = new TimeModule.Date(2019, 12, 1);
    public TimeModule.Time GameTime { get; private set; } = new TimeModule.Time(0, 0, 0);
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
         _generateInfections.Invoke();
        //Change the Game Speed;
        GameTime += new TimeModule.Time(0, _changeMinutes[GameSpeed] * Time.deltaTime, 0);
        if (GameTime.Hour >= 24)
        {
            //Generate Infections
            GameTime.Hour = 0;
            GameDate += new TimeModule.Date(0, 0, 1);
        }
    }
}
