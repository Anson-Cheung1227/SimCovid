using UnityEngine;
using System;

public class DataManager : MonoBehaviour 
{
    public static DataManager Instance; 
    public DateTime GameDateTime;
    public bool ActiveStateDetailsPanel;
    public State HoveringState;
    public State SelectedState;
    public float RecoveryRate;
    public float DeathRate;
    [SerializeField] private GlobalStatsSO _globalStatsSO;  
    private void Awake() 
    {
        Instance = this;     
    }
    void Start()
    {
        GameDateTime = new DateTime(2019,12,1,1,1,1);
        SelectedState = null;
        RecoveryRate = _globalStatsSO.RecoveryRate;
        DeathRate = _globalStatsSO.DeathRate;
    }  
}