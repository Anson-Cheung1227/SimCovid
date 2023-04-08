using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimCovid.Core
{
    public class GameEventManager : MonoBehaviour
    {
        public static GameEventManager Instance;
        public event Action<int> OnSetSceneId;
        public event Action OnDateChange;
        public event Action<DataManager> OnGenerateInfection;
        public event Action<DataManager> OnGenerateInHospital;
        public event Action<DataManager> OnGenerateRecovery;
        public event Action<DataManager> OnGenerateDeath;
        public event Action OnUpdateMorale;
        public event Action<DataManager> OnUpdateUI;
        public event Action<string, Sprite, string, string> OnActiveModalWindow;
        public event Action<List<State>> OnUpdateAllStatesDetailsTable;
        private void Awake()
        {
            Instance = this;
        }
        public void InvokeOnSetSceneId(int id)
        {
            if (OnSetSceneId != null)
            {
                OnSetSceneId(id);
            }
        }
        public void InvokeOnDateChange()
        {
            if (OnDateChange != null)
            {
                OnDateChange();
            }
        }
        public void InvokeOnGenerateInfection(DataManager dataManager)
        {
            if (OnGenerateInfection != null)
            {
                OnGenerateInfection(dataManager);
            }
        }
        public void InvokeOnGenerateInHospital(DataManager dataManager)
        {
            if (OnGenerateInHospital != null)
            {
                OnGenerateInHospital(dataManager);
            }
        }
        public void InvokeOnGenerateRecovery(DataManager dataManager)
        {
            if (OnGenerateRecovery != null)
            {
                OnGenerateRecovery(dataManager);
            }
        }
        public void InvokeOnGenerateDeath(DataManager dataManager)
        {
            if (OnGenerateDeath != null)
            {
                OnGenerateDeath(dataManager);
            }
        }
        public void InvokeOnUpdateMorale()
        {
            if (OnUpdateMorale != null)
            {
                OnUpdateMorale();
            }
        }
        public void InvokeOnUpdateUI(DataManager dataManager)
        {
            if (OnUpdateUI != null)
            {
                OnUpdateUI(dataManager);
            }
        }
        public void InvokeOnActiveModalWindow(string header, Sprite image, string contentString, string buttonText)
        {
            if (OnActiveModalWindow != null)
            {
                OnActiveModalWindow(header, image, contentString, buttonText);
            }
        }
        public void InvokeOnUpdateAllStateDetailsTable(List<State> stateList)
        {
            if (OnUpdateAllStatesDetailsTable != null)
            {
                OnUpdateAllStatesDetailsTable(stateList);
            }
        }
    }
}
