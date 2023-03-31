using System.Collections.Generic;
using UnityEngine;
using InfectionModule;
using ISimCovid;
using Random = UnityEngine.Random;

/// <summary>
/// Generates Infections
/// </summary>
public class InfectionGeneration : MonoBehaviour
{
    //References
    [SerializeField] private DataManager _dataManager;
    [SerializeField] private List<StateController> _allState = new List<StateController>();
    private List<State> _allStates = new List<State>();
    //For Unity Editor use only
    [SerializeField] private long totalInfection = 0;
    private void Start()
    {
        GameEventManager.Instance.OnGenerateInfection += GenerateInfection;
        foreach (StateController stateController in _allState)
        {
            _allStates.Add(stateController.State);
        }
        UpdateInfectionList(_dataManager.StateInfectionsTable, _allStates);
    }
    //UnityEvent for adding infections, called once per day
    public void GenerateInfection(DataManager dataManager)
    {
        GenerateAllInfectionsLocal(_allStates);
        GenerateAllInfectionsInterstate(_allStates);
        GenerateInfectionsGlobal(_allStates);
        UpdateInfectionList(dataManager.StateInfectionsTable, _allStates);
    }
    public void GenerateAllInfectionsLocal(List<State> states)
    {
        foreach (State state in states)
        {
            if (!state.LocalLockdown)
            {
                GenerateInfectionsLocal(state.InfectionManager.GetActive());
            }
        }
    }
    public void GenerateAllInfectionsInterstate(List<State> states)
    {
        List<ISpreadableDataHandler<Infection>> eligibleStates = new List<ISpreadableDataHandler<Infection>>();
        foreach (State state in states)
        {
            if (!state.InterstateLockdown)
            {
                eligibleStates.Add(state.InfectionManager.GetActive());
            }
        }
        GenerateInfectionsInterstate(eligibleStates);
    }
    public void GenerateInfectionsLocal<ISpreadableTarget>(ISpreadableDataHandler<ISpreadableTarget> activeSpreadableDataHandler) where ISpreadableTarget: class, ISpreadable, new()
    {
        long generateAmount = activeSpreadableDataHandler.GetActualInfectionsCount();
        ISpreadableTarget spreadableTarget = new ISpreadableTarget();
        AddInfection(activeSpreadableDataHandler, spreadableTarget);
    }
    public void GenerateInfectionsInterstate<ISpreadableTarget>(List<ISpreadableDataHandler<ISpreadableTarget>> activeSpreadableDataHandler) where ISpreadableTarget : class, ISpreadable, new()
    {
        /*
            We do not want new generated infections to pass on immediately, else we'll be in an infinite loop
            We will add all infections at once after the calculations
        */
        ISpreadableDataHandler<ISpreadableTarget> target;
        Dictionary<ISpreadableDataHandler<ISpreadableTarget>, long> delayedInfection = new Dictionary<ISpreadableDataHandler<ISpreadableTarget>, long>();
        foreach (ISpreadableDataHandler<ISpreadableTarget> spreadableDataHandler in activeSpreadableDataHandler)
        {
            target = DetermineTargetInfectionInterstate(activeSpreadableDataHandler);
            if (delayedInfection.ContainsKey(target))
            {
                delayedInfection[target] += target.GetActualInfectionsCount();
            }
            else
                delayedInfection.Add(target, target.GetActualInfectionsCount());
        }
        //Loop through the delayedInfection Dictionary, and add infections
        foreach (KeyValuePair<ISpreadableDataHandler<ISpreadableTarget>, long> infection in delayedInfection)
        {
            ISpreadableTarget newInfection = new ISpreadableTarget();
            AddInfection(infection.Key, newInfection);
        }
    }
    public ISpreadableDataHandler<ISpreadableTarget> DetermineTargetInfectionInterstate<ISpreadableTarget>(List<ISpreadableDataHandler<ISpreadableTarget>> list) where ISpreadableTarget : class, ISpreadable, new()
    {
        //TODO: Take in how likely the people are gonna travel
        return list[Random.Range(0, list.Count)];
    }
    public void GenerateInfectionsGlobal(List<State> states)
    {
        List<ISpreadableDataHandler<Infection>> list = new List<ISpreadableDataHandler<Infection>>();
        foreach (State state in states)
        {
            list.Add(state.InfectionManager.GetActive());
        }
        ISpreadableDataHandler<Infection> target = DetermineStateInfectionGlobal(list);
        Infection infection = new Infection();
        AddInfection(target, infection);
    }
    public ISpreadableDataHandler<ISpreadableTarget> DetermineStateInfectionGlobal<ISpreadableTarget>(List<ISpreadableDataHandler<ISpreadableTarget>> list) where ISpreadableTarget : class, ISpreadable, new()
    {
        return list[Random.Range(0, list.Count)];
    }
    public void AddInfection<ISpreadableTarget>(ISpreadableDataHandler<ISpreadableTarget> spreadableDataHandler, ISpreadableTarget param ,long infections = 1) where ISpreadableTarget : class ,ISpreadable, new()
    {
        ISpreadable foundResult = spreadableDataHandler.FindExistingInstance(param);
        if (foundResult == null)
        {
            spreadableDataHandler.AddISpreadable(param);
        }
        else
        {
            foundResult.AddToInfection(infections);
        }
    }
    private void UpdateInfectionList(List<State> list, List<State> refState)
    {
        list.Clear();
        foreach (State state in refState)
        {
            if (list.Count == 0)
            {
                list.Add(state);
                continue;
            }

            int iter = list.Count;
            while (state.InfectionManager.GetTotalInfections() > list[iter - 1].InfectionManager.GetTotalInfections())
            {
                iter--;
                if (iter == 0) break;
            }
            list.Insert(iter, state);
        }
    }
}