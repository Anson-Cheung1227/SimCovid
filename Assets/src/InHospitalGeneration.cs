using UnityEngine;
using System;
using System.Collections.Generic;
using InfectionModule;
using Unity.Profiling;
public class InHospitalGeneration : MonoBehaviour 
{
    [SerializeField] private TimeController _timeController; 
    [SerializeField] private List<StateController> _allStates;
    private int[] _dayList = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
    static readonly ProfilerMarker s_GenerateInHospitalMarker = new ProfilerMarker("GenerateInHospital");
    private void Start() 
    {
        GameEventManager.Instance.OnGenerateInHospital += GenerateInHospital;
    }
    private void Update() 
    {
        
    }
    public void GenerateInHospital(DataManager dataManager)
    {
        s_GenerateInHospitalMarker.Begin();
        foreach (StateController stateController in _allStates)
        {
            Infection findResult;
            if (stateController.State.Infections.Count == 0) continue;
            foreach (Infection infection in stateController.State.ActiveInfections)
            {
                int daysSinceInfection = (int)((dataManager.GameDateTime.Date - infection.Date.Date).TotalDays);
                if (daysSinceInfection <= 1) continue;
                int chance = 100 - 100 / daysSinceInfection;
                long generateAmount = (int)(infection.Amount * chance/100);
                if (generateAmount < 1) continue;
                findResult = Infection.FindExistingInfection(stateController.State, infection.Date, dataManager.GameDateTime, null, null,InfectionStatus.InHospital, infection.HasSpread);
                if (findResult == null)
                {
                    findResult = new Infection { Date = infection.Date, InHospitalDate = dataManager.GameDateTime, InfectionStatus = InfectionStatus.InHospital, Amount = generateAmount, HasSpread = infection.HasSpread};
                    stateController.State.InHospital.Add(findResult);
                    stateController.State.Infections.Add(findResult);
                }
                else
                {
                    findResult.Amount += generateAmount;
                }
                infection.Amount -= generateAmount;
                stateController.State.ActiveInfectionsLong -= generateAmount;
                stateController.State.InHospitalLong += generateAmount;
            }
        }
        s_GenerateInHospitalMarker.End();
    }
}