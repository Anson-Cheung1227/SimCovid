using UnityEngine;
using System;
using System.Collections.Generic;

public class DataManager : MonoBehaviour 
{
    public DateTime GameDateTime = new DateTime(2019, 12, 1, 0, 0, 0);
    public bool ActiveStateDetailsPanel;
    public State HoveringState;
    public State SelectedState = null;
    public float RecoveryRate;
    public float DeathRate;
    [SerializeField] private GlobalStatsSO _globalStatsSO;
    public List<State> StateInfectionsTable = new List<State>();
    private void Awake() 
    {
        GameManager.Instance.DataManagerList.Add(this);
    }
    void Start()
    {
        RecoveryRate = _globalStatsSO.RecoveryRate;
        DeathRate = _globalStatsSO.DeathRate;
    }  
}