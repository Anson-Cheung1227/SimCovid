using UnityEngine;
using System.Collections.Generic;
using InfectionModule;
using Unity.Profiling;
public class InHospitalGeneration : MonoBehaviour 
{
    [SerializeField] private TimeController _timeController; 
    [SerializeField] private List<StateController> _allStates = new List<StateController>();
    private int[] _dayList = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
    static readonly ProfilerMarker s_GenerateInHospitalMarker = new ProfilerMarker("GenerateInHospital");
    private void Start() 
    {
        
    }
    private void Update() 
    {
        
    }
    public void GenerateInHospital()
    {
        s_GenerateInHospitalMarker.Begin();
        foreach (StateController stateController in _allStates)
        {
            Infection findResult;
            if (stateController.State.Infections.Count == 0) continue;
            foreach (Infection infection in stateController.State.ActiveInfections)
            {
                int daysSinceInfection = (int)(_timeController.GameDate.Day - infection.Date.Day);
                if (daysSinceInfection < 0)
                {
                    daysSinceInfection += _dayList[(int)infection.Date.Month - 1];
                }
                if (daysSinceInfection <= 4) continue;
                int chance = 100 - 100 / daysSinceInfection;
                long generateAmount = (int)infection.Amount * chance/100;
                if (generateAmount < 1) continue;
                findResult = Infection.FindExistingInfection(stateController.State, infection.Date, InfectionStatus.InHospital);
                if (findResult == null)
                {
                    findResult = new Infection { Date = infection.Date, InfectionStatus = InfectionStatus.InHospital, Amount = generateAmount};
                    stateController.State.InHospital.Add(infection);
                    infection.Amount -= generateAmount;
                }
                else
                {
                    findResult.Amount += generateAmount;
                }
            }
        }
        s_GenerateInHospitalMarker.End();
    }
}