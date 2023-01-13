using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionGeneration : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TimeController _timeController; 
    [SerializeField] private List<StateController> _allState = new List<StateController>();
    [SerializeField] private List<State> _stateInfections = new List<State>();
    private List<Airport> _allStateAirports = new List<Airport>();
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        _stateInfections.Clear();
        foreach (StateController stateController in _allState)
        {
            if (_stateInfections.Count == 0) 
            {
                _stateInfections.Add(stateController.State);
                continue;
            }
            int iter = _stateInfections.Count;
            while (stateController.State.Infections.Count > _stateInfections[iter - 1].Infections.Count)
            {
                iter--;
                if (iter == 0) break;
            }
            _stateInfections.Insert(iter, stateController.State);
        }
    }
    //UnityEvent
    public void GenerateInfection()
    {
        Dictionary<List<State>, long> delayedInfection = new Dictionary<List<State>, long>();
        AddInfection(DetermineStateInfectionGlobal(), InfectionType.Global);
        foreach (StateController stateController in _allState)
        {
            if (stateController.State.Infections.Count - stateController.State.InHospital - stateController.State.Recovered == 0) continue;
            List<State> newInfectState = new List<State>();
            newInfectState.Add(DetermineStateInfectionInterstate());
            newInfectState.Add(stateController.State);
            delayedInfection.Add(newInfectState, stateController.State.Infections.Count - stateController.State.InHospital - stateController.State.Recovered);
        }
        foreach (KeyValuePair<List<State>, long> infection in delayedInfection)
        {
            AddInfection(infection.Key[0], InfectionType.Interstate, infection.Key[1], infection.Value);
        }
    }
    public State DetermineStateInfectionInterstate()
    {
        //TODO: Take in how likely the people are gonna travel
        return _allState[Random.Range(0, _allState.Count - 1)].State;
    }
    public State DetermineStateInfectionGlobal()
    {
        //TODO: Take in how likely the people are gonna travel 
        State determinedState = null;
        long DailyTransportedPeople = 0;
        Dictionary<long, State> transportedPassengers = new Dictionary<long, State>();
        foreach (StateController stateController in _allState)
        {
            if (stateController.State.DailyIncomingPeople == 0) continue;
            DailyTransportedPeople += stateController.State.DailyIncomingPeople;
            transportedPassengers.Add(DailyTransportedPeople, stateController.State);
        }
        long person = (long)Random.Range(1L, DailyTransportedPeople);
        foreach (KeyValuePair<long, State> entry in transportedPassengers)
        {
            if (person < entry.Key)
            {
                determinedState = entry.Value;
                break;
            }
        }
        return determinedState;
    }
    public void AddInfection(State state, InfectionType infectionType, State originState = null, long infections = 1)
    {
        if (infectionType == InfectionType.Interstate)
        {
            for (int i = 0; i < infections; ++i)
            {
                state.Infections.Add(new Infection{Date = _timeController.GameDate});
            }
            Debug.Log($"{state.Name}: {infectionType.ToString()}: Generated {infections} infections from {originState.Name}");
        }
        if (infectionType == InfectionType.Global)
        {
            for (int i = 0; i < infections; ++i)
            {
                state.Infections.Add(new Infection());
            }
            Debug.Log($"{state.Name}: {infectionType.ToString()}: Generated {infections} infections");
        }
    }
}
