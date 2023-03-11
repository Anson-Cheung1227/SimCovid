using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class StateController : MonoBehaviour 
{
    private class Initialization : ILoadOperation
    {
        public string Name { get; set; }
        public float Operations { get; set; }
        public float DoneOperations { get; set; }
        public MonoBehaviour Operator { get; set; }
        public StateSO StateTemplate;
        public void Load()
        {
            ((StateController)Operator).State = (State)StateTemplate;
            foreach (Airport airport in ((StateController)Operator).State.AirportList)
            {
                ((StateController)Operator).State.DailyIncomingPeople += airport.YearlyPassengers / 365;
            }
            DoneOperations = Operations;
        }
    }
    [field: SerializeField] public State State { get; private set; }
    [SerializeField] private StateSO _stateTemplate;
    [SerializeField] private AllStatesManager _allStatesManager;
    Initialization _initialization;
    void Awake()
    {
        StartCoroutine(AddToAllStatesManager());
    }
    private void Start()
    {
        _initialization.StateTemplate = _stateTemplate;
        _initialization.Load();
    }
    private IEnumerator AddToAllStatesManager()
    {
        while (_allStatesManager == null) yield return null;
        _initialization = new Initialization
        {
            Operations = 1,
            DoneOperations = 0,
            Operator = this,
        };
        _allStatesManager.AllstateLoadOperations.Add(_initialization);
    }
}