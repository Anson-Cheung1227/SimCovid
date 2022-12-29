using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionGeneration : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<StateController> _allState = new List<StateController>();
    private List<Airport> _allStateAirports = new List<Airport>();
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    //UnityEvent
    public void GenerateInfection()
    {
        AddInfection(DetermineStateInfection(InfectionType.Global), InfectionType.Global);
    }
    public State DetermineStateInfection(InfectionType infectionType)
    {
        if (infectionType == InfectionType.Global)
        {
            long DailyTransportedPeople = 0; 
            Dictionary<long, State> transportedPassengers = new Dictionary<long, State>();
            State determinedState = null; 
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
        return null; 
    }
    public void AddInfection(State state, InfectionType infectionType, int infections = 1)
    {
        if (infectionType == InfectionType.Global)
        {
            state.Infections++; 
            Debug.Log($"{state.Name}: {infectionType.ToString()}: Generated {infections} infections");
        }
    }   
}
