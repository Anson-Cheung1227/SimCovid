using UnityEngine;

public class DataManager : MonoBehaviour 
{
    public static DataManager Instance; 
    public TimeModule.Time GameTime;
    public TimeModule.Date GameDate;
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
        GameTime = new TimeModule.Time(0,0,0);
        GameDate = new TimeModule.Date(2019, 12, 1);
        SelectedState = null;
        RecoveryRate = _globalStatsSO.RecoveryRate;
        DeathRate = _globalStatsSO.DeathRate;
    }  
}