using UnityEngine;
using System.Collections.Generic;

public class InHospitalGeneration : MonoBehaviour 
{
    [SerializeField] private TimeController _timeController; 
    [SerializeField] private List<StateController> _allStates = new List<StateController>();
    private int[] _dayList = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
    private void Start() 
    {
        
    }
    private void Update() 
    {
        
    }
    public void GenerateInHospital()
    {
        foreach (StateController stateController in _allStates)
        {
            if (stateController.State.Infections.Count == 0) continue;
            foreach (Infection infection in stateController.State.Infections)
            {
                if (infection.InfectionStatus != InfectionStatus.Active) continue;
                int daysSinceInfection = (int)(_timeController.GameDate.Day - infection.Date.Day);
                if (daysSinceInfection < 0)
                {
                    daysSinceInfection += _dayList[(int)infection.Date.Month - 1]; 
                }
                if (daysSinceInfection == 0) continue;
                int chance = 100 - 100/daysSinceInfection;
                if (chance >= Random.Range(0, 101))
                {
                    //TODO:Move infection to InHospital
                    infection.InfectionStatus = InfectionStatus.InHospital;
                    stateController.State.InHospital.Add(infection);
                    Debug.Log($"{stateController.State.Name}: Transferred 1 infection to InHospital");
                }
            }
        }
    }
}