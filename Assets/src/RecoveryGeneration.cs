using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InfectionModule;

public class RecoveryGeneration : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<StateController> _allStates;
    [SerializeField] private DataManager _dataManager;
    [SerializeField] private TimeController _timeController;
    void Start()
    {
        
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
                int generatedAmount = (int)(infection.Amount * _dataManager.RecoveryRate);
                findResult = Infection.FindExistingInfection(stateController.State, _timeController.GameDate, InfectionStatus.Recovered, infection.HasSpread);
                if (findResult == null)
                {
                    findResult = new Infection{Date = _timeController.GameDate, InfectionStatus = InfectionStatus.Recovered, Amount = generatedAmount};
                    stateController.State.Recovered.Add(findResult);
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
