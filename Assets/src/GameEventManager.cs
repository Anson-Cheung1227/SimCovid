using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager Instance;
    public event Action OnDateChange;
    public event Action OnGenerateInfection; 
    public event Action OnGenerateInHospital;
    public event Action OnGenerateRecovery;
    public event Action OnGenerateDeath;
    public event Action OnUpdateMorale; 
    public event Action OnUpdateUI;
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
    public void InvokeOnGenerateInfection()
    {
        if (OnGenerateInfection != null)
        {
            OnGenerateInfection();   
        }
    }
    public void InvokeOnGenerateInHospital()
    {
        if (OnGenerateInHospital != null)
        {
            OnGenerateInHospital();   
        }
    }
    public void InvokeOnGenerateRecovery()
    {
        if (OnGenerateRecovery != null)
        {
            OnGenerateRecovery();   
        }
    }
    public void InvokeOnGenerateDeath()
    {
        if (OnGenerateDeath != null)
        {
            OnGenerateDeath();
        }
    }
    public void InvokeOnUpdateMorale()
    {
        if (OnUpdateMorale != null)
        {
            OnUpdateMorale();
        }
    }
    public void InvokeOnUpdateUI()
    {
        if (OnUpdateUI != null)
        {
            OnUpdateUI();
        }
    }
}
