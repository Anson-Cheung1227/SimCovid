using System;
using UnityEngine;

namespace SimCovid.Core
{
    /// <summary>
    /// Controls the time of the Game
    /// </summary>
    public class TimeController : MonoBehaviour
    {
        [SerializeField] private DataManager _dataManager;
        [SerializeField] private float[] _changeMinutes = new float[5];
        private Nullable<DateTime> _lastUpdateDateTime;
        //public Property to control the speed of time;
        public int GameSpeed { get; private set; }
        private void Start()
        {
            _lastUpdateDateTime = _dataManager.GameDateTime;
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
            _dataManager.GameDateTime = _dataManager.GameDateTime.AddMinutes(_changeMinutes[GameSpeed] * Time.deltaTime);
            if (_dataManager.GameDateTime.Date != _lastUpdateDateTime.Value.Date)
            {
                GameEventManager.Instance.InvokeOnDateChange();
                GameEventManager.Instance.InvokeOnGenerateInfection(_dataManager);
                GameEventManager.Instance.InvokeOnGenerateInHospital(_dataManager);
                GameEventManager.Instance.InvokeOnGenerateRecovery(_dataManager);
                GameEventManager.Instance.InvokeOnGenerateDeath(_dataManager);
                GameEventManager.Instance.InvokeOnUpdateMorale();
                _lastUpdateDateTime = _dataManager.GameDateTime;
                GameEventManager.Instance.InvokeOnActiveModalWindow("Day has changed!", null, null, "Cool!");
            }
            GameEventManager.Instance.InvokeOnUpdateUI(_dataManager);
        }
    }
}
