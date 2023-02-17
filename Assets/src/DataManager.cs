using UnityEngine;
using System;

public class DataManager : MonoBehaviour 
{
    public static DataManager Instance; 
    public DateTime GameDateTime = new DateTime(2019, 12, 1, 0, 0, 0);
    public bool ActiveStateDetailsPanel;
    public State HoveringState;
    public State SelectedState = null;
    public float RecoveryRate;
    public float DeathRate;
    [SerializeField] private GlobalStatsSO _globalStatsSO;  
    private void Awake() 
    {
        Instance = this;     
    }
    void Start()
    {
        RecoveryRate = _globalStatsSO.RecoveryRate;
        DeathRate = _globalStatsSO.DeathRate;
    }  
}