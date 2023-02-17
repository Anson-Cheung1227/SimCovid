using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InfectionModule;

public class RecoveryGeneration : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<StateController> _allStates;
    void Start()
    {
        GameEventManager.Instance.OnGenerateRecovery += GenerateRecovery;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GenerateRecovery()
    {
        Infection findResult;
        foreach (StateController stateController in _allStates)
        {
            foreach (Infection infection in stateController.State.InHospital)
            {
                int daysSinceInHospital = (int)((DataManager.Instance.GameDateTime.Date - infection.Date.Date).TotalDays);
                if (daysSinceInHospital < 14) continue;
                int generatedAmount = (int)(infection.Amount * DataManager.Instance.RecoveryRate);
                if (generatedAmount < 1) continue;
                findResult = Infection.FindExistingInfection(stateController.State, infection.Date, infection.InHospitalDate, DataManager.Instance.GameDateTime, null,InfectionStatus.Recovered, infection.HasSpread);
                if (findResult == null)
                {
                    findResult = new Infection{Date = infection.Date, InHospitalDate = infection.InHospitalDate, RecoveryDate = DataManager.Instance.GameDateTime, InfectionStatus = InfectionStatus.Recovered, Amount = generatedAmount, HasSpread = infection.HasSpread};
                    stateController.State.Recovered.Add(findResult);
                    stateController.State.Infections.Add(findResult);
                }
                else
                {
                    findResult.Amount += generatedAmount;
                }
                infection.Amount -= generatedAmount;
                stateController.State.InHospitalLong -= generatedAmount;
                stateController.State.RecoveredLong += generatedAmount;
            }
        }
    }
}
