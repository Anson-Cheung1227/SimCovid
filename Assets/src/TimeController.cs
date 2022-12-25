using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField] private float[] _changeMinutes = new float[5];
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
        //Change the Game Speed;
        GameTime += new TimeModule.Time(0, _changeMinutes[GameSpeed] * Time.deltaTime, 0);
        if (GameTime.Hour >= 24)
        {
            GameTime.Hour = 0;
            Debug.Log((string)GameDate);
            GameDate += new TimeModule.Date(0, 0, 1);
        }
    }
}
