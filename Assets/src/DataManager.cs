using UnityEngine;

public class DataManager : MonoBehaviour 
{
    public State SelectedState;
    public float RecoveryRate;
    public float DeathRate;
    [SerializeField] private GlobalStatsSO _globalStatsSO;  
    void Start()
    {
        SelectedState = null;
        RecoveryRate = _globalStatsSO.RecoveryRate;
        DeathRate = _globalStatsSO.DeathRate;
    }  
}