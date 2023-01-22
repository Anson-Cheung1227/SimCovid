using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionGeneration : MonoBehaviour
{
    // Start is called before the first frame update
    //Reference to TimeController
    [SerializeField] private TimeController _timeController; 
    //All References to StateControllers for State data
    [SerializeField] private List<StateController> _allState = new List<StateController>();
    //For Unity Editor use only
    [SerializeField] private List<State> _stateInfections = new List<State>();
    [SerializeField] private long totalInfection = 0;
    //Reference to all Airports
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
    //UnityEvent for adding infections, called once per day
    public void GenerateInfection()
    {
        /*
            We do not want new generated infections to pass on immediately, else we'll be in an infinite loop
            We will add all infections at once after the calculations
        */
        #region Local Infections
        foreach (StateController stateController in _allState)
        {
            if (stateController.State.ActiveInfections.Count == 0) continue;
            AddInfection(stateController.State, InfectionType.Local, infections: (long)(stateController.State.ActiveInfections.Count * stateController.State.localSpreadRate));
        }
        #endregion Local Infections
        #region Global Infections
        Dictionary<List<State>, long> delayedInfection = new Dictionary<List<State>, long>();
        //Adding one global infection
        AddInfection(DetermineStateInfectionGlobal(), InfectionType.Global);
        #endregion Global Infections
        #region Interstate Infections
        //Adding Interstate infections
        foreach (StateController stateController in _allState)
        {
            //If there is no active infections in the state, continue checks for the next state
            if (stateController.State.ActiveInfections.Count == 0) continue;
            //else, there will be a spread of infections, interstate
            List<State> newInfectState = new List<State>();
            //Determine the target state
            newInfectState.Add(DetermineStateInfectionInterstate());
            //Pass the origin state
            newInfectState.Add(stateController.State);
            //Add to infections queue
            delayedInfection.Add(newInfectState, stateController.State.ActiveInfections.Count);
        }
        //Loop through the delayedInfection Dictionary, and add infections
        foreach (KeyValuePair<List<State>, long> infection in delayedInfection)
        {
            AddInfection(infection.Key[0], InfectionType.Interstate, infection.Key[1], infection.Value);
        }
        #endregion Interstate Infections
    }
    public State DetermineStateInfectionInterstate()
    {
        //TODO: Take in how likely the people are gonna travel
        return _allState[Random.Range(0, _allState.Count - 1)].State;
    }
    public State DetermineStateInfectionGlobal()
    {
        //TODO: Take in how likely the people are gonna travel 
        /*
            This Global infection generation method is temporary,
            Right now it generates global infection in accordance with the daily transported people, 
            The higher the daily transported people is, the higher chance it will generate an global infection
        */
        State determinedState = null;
        //For calculating the total Transported people per day
        long DailyTransportedPeople = 0;
        Dictionary<long, State> transportedPassengers = new Dictionary<long, State>();
        foreach (StateController stateController in _allState)
        {
            //If the dailyIncomingPeople is 0, we move on to the next state
            if (stateController.State.DailyIncomingPeople == 0) continue;
            //else, we add the DailyIncomingPeople to the total DailyTransportedPeople
            DailyTransportedPeople += stateController.State.DailyIncomingPeople;
            transportedPassengers.Add(DailyTransportedPeople, stateController.State);
        }
        //Determine which person will be infected
        long person = (long)Random.Range(1L, DailyTransportedPeople);
        //Look up which State the person is arriving to
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
        if (infectionType == InfectionType.Local)
        {
            for (int i = 0; i < infections; ++i)
            {
                Infection infection = new Infection{Date = _timeController.GameDate};
                state.Infections.Add(infection);
                state.ActiveInfections.Add(infection);
            }
            //Debug.Log($"{state.Name}: {infectionType.ToString()}: Generated {infections} infection");
        }
        if (infectionType == InfectionType.Interstate)
        {
            for (int i = 0; i < infections; ++i)
            { 
                Infection infection = new Infection{Date = _timeController.GameDate};
                state.Infections.Add(infection);
                state.ActiveInfections.Add(infection);
            }
            //Debug.Log($"{state.Name}: {infectionType.ToString()}: Generated {infections} infections from {originState.Name}");
        }
        if (infectionType == InfectionType.Global)
        {
            for (int i = 0; i < infections; ++i)
            {
                Infection infection = new Infection{Date = _timeController.GameDate};
                state.Infections.Add(infection);
                state.ActiveInfections.Add(infection);
            }
            //Debug.Log($"{state.Name}: {infectionType.ToString()}: Generated {infections} infections");
        }
        totalInfection += infections; 
    }
}