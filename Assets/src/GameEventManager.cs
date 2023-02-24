using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager Instance;
    public event Action OnDateChange;
    public event Action<DataManager> OnGenerateInfection; 
    public event Action<DataManager> OnGenerateInHospital;
    public event Action<DataManager> OnGenerateRecovery;
    public event Action<DataManager> OnGenerateDeath;
    public event Action OnUpdateMorale; 
    public event Action<DataManager> OnUpdateUI;
    private void Awake() 
    {
        Instance = this;    
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
}
