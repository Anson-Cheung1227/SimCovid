using System.Collections;
using System.Threading.Tasks;
using SimCovid.Core.Infection;
using SimCovid.Core.Policies;
using SimCovidAPI;
using UnityEngine;

namespace SimCovid.Core
{
    /// <summary>
    /// StateContoller for every State
    /// </summary>
    public class StateController : MonoBehaviour
    {
        /// <summary>
        /// For Initialization use, implements ILoadOperation, call Load() to start loading
        /// </summary>
        private class Initialization : ILoadOperation
        {
            public string Name { get; set; }
            public long Operations { get; set; }
            public long DoneOperations { get; set; }
            public MonoBehaviour Operator { get; set; }
            public StateSO StateTemplate;
            public Task Load()
            {
                ((StateController)Operator).State = (State)StateTemplate;
                foreach (Airport airport in ((StateController)Operator).State.AirportList)
                {
                    ((StateController)Operator).State.DailyIncomingPeople += airport.YearlyPassengers / 365;
                }

                long population = ((StateController)Operator).State.Population;
                ((StateController)Operator).State.InfectionManager = new InfectionManager(population);
                ((StateController)Operator).State.PolicyManager = new PolicyManager(new LockdownPolicy());
                ((StateController)Operator).State.EligibilityManager =
                    new EligibilityManager(((StateController)Operator).State);
                DoneOperations = Operations;
                return Task.CompletedTask;
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
        /// <summary>
        /// Every Frame this will check if AllStatesManager is null (not initailized), if it's initialized, add Initialization class to AllStatesManager
        /// </summary>
        /// <returns></returns>
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
}