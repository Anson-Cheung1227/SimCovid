using UnityEngine;

public class StateController : MonoBehaviour 
{
    [field: SerializeField] public State State {get; private set;}
    [SerializeField] private StateSO _stateTemplate; 
    void Awake()
    {
        State = (State)_stateTemplate;
        foreach (Airport airport in State.AirportList)
        {
            State.DailyIncomingPeople += airport.YearlyPassengers/365;
        }
    }
}