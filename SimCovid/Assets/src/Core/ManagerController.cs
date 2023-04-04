using System;
using UnityEngine;
using System.Collections.Generic;
using SimCovidAPI;

namespace Core
{
    public class ManagerController : MonoBehaviour
    {
        [SerializeField] private DataManager _dataManager;
        private List<IManager> _allManagers = new List<IManager>();
        [SerializeField] private List<StateController> _allStateControllers;
        private List<ILocation> _states = new List<ILocation>();

        private void Start()
        {
            GameEventManager.Instance.OnGenerateInfection += OnGenerateInfection;
            foreach (StateController stateController in _allStateControllers)
            {
                _states.Add(stateController.State);
            }
            AddIManager(new InfectionGeneration(_states, _dataManager));
        }

        private void OnGenerateInfection(DataManager dataManager)
        {
            foreach (IManager manager in _allManagers)
            {
                manager.Execute();
            }
        }

        public void AddIManager(IManager manager)
        {
            _allManagers.Add(manager);
        }
    }
}