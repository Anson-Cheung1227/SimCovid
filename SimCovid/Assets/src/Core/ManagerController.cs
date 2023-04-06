﻿using System;
using UnityEngine;
using System.Collections.Generic;
using SimCovidAPI;

namespace Core
{
    public class ManagerController : MonoBehaviour
    {
        public static ManagerController Instance;
        [SerializeField] private DataManager _dataManager;
        [SerializeField] private List<StateController> _allStateControllers;
        private List<ILocation> _states = new List<ILocation>();

        private List<ISpreadableGenerationManager> _spreadableGenerationManagers =
            new List<ISpreadableGenerationManager>();

        private void Awake()
        {
            Instance = this;
            GameEventManager.Instance.OnGenerateInfection += delegate(DataManager dataManager)
            {
                foreach (ISpreadableGenerationManager spreadableGenerationManager in _spreadableGenerationManagers)
                {
                    spreadableGenerationManager.OnGenerate();
                }
            };
        }

        private void Start()
        {
            foreach (StateController stateController in _allStateControllers)
            {
                _states.Add(stateController.State);
            }
            AddManager(new InfectionGeneration(_states, _dataManager));
        }

        public void AddManager(ISpreadableGenerationManager manager)
        {
            _spreadableGenerationManagers.Add(manager);
        }
    }
}