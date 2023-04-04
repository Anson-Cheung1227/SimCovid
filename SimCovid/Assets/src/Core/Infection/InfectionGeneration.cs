using System.Collections.Generic;
using UnityEngine;
using InfectionModule;
using SimCovidAPI;
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
        GenerateAllInfectionsGlobal(_allStates);
        UpdateInfectionList(dataManager.StateInfectionsTable, _allStates);
    }

    private void GenerateAllInfectionsLocal(List<State> states)
    {
        foreach (State state in states)
        {
            if (!state.LocalLockdown)
            {
                GenerateInfectionsLocal(state.InfectionManager.GetActive());
            }
        }
    }

    private void GenerateAllInfectionsInterstate(List<State> states)
    {
        List<ISpreadableDataHandler> eligibleStates =
            new List<ISpreadableDataHandler>();
        foreach (State state in states)
        {
            if (!state.InterstateLockdown)
            {
                eligibleStates.Add((ISpreadableDataHandler)state.InfectionManager.GetActive());
            }
        }
        GenerateInfectionsInterstate(eligibleStates);
    }

    public void GenerateInfectionsLocal(ISpreadableDataHandler activeSpreadableDataHandler)
    {
        ISpreadable newInfection = activeSpreadableDataHandler.CreateISpreadable();
        newInfection.AddToInfection(activeSpreadableDataHandler.GetActualInfectionsCount());
        AddInfection(activeSpreadableDataHandler, newInfection);
    }

    public void GenerateInfectionsInterstate(List<ISpreadableDataHandler> activeSpreadableDataHandler)
    {
        /*
            We do not want new generated infections to pass on immediately, else we'll be in an infinite loop
            We will add all infections at once after the calculations
        */
        ISpreadableDataHandler target;
        Dictionary<ISpreadableDataHandler, long> delayedInfection = new Dictionary<ISpreadableDataHandler, long>();
        foreach (ISpreadableDataHandler spreadableDataHandler in activeSpreadableDataHandler)
        {
            if (spreadableDataHandler.GetActualInfectionsCount() == 0) continue;
            target = DetermineTargetInfectionInterstate(activeSpreadableDataHandler);
            if (delayedInfection.ContainsKey(target))
                delayedInfection[target] += spreadableDataHandler.GetActualInfectionsCount();
            else
                delayedInfection.Add(target, spreadableDataHandler.GetActualInfectionsCount());
        }

        //Loop through the delayedInfection Dictionary, and add infections
        foreach (KeyValuePair<ISpreadableDataHandler, long> infection in delayedInfection)
        {
            ISpreadable newInfection = infection.Key.CreateISpreadable();
            newInfection.AddToInfection(infection.Value);
            AddInfection(infection.Key, newInfection);
        }
    }

    public ISpreadableDataHandler DetermineTargetInfectionInterstate(List<ISpreadableDataHandler> list) 
    {
        //TODO: Take in how likely the people are gonna travel
        return list[Random.Range(0, list.Count)];
    }

    private void GenerateAllInfectionsGlobal(List<State> stateList)
    {
        List<ISpreadableDataHandler> spreadableDataHandlers = new List<ISpreadableDataHandler>();
        foreach (State state in stateList)
        {
            spreadableDataHandlers.Add(state.InfectionManager.GetActive());
        }

        GenerateInfectionsGlobal(spreadableDataHandlers);
    }

    public void GenerateInfectionsGlobal(List<ISpreadableDataHandler> list)
    {
        ISpreadableDataHandler target = DetermineStateInfectionGlobal(list);
        ISpreadable infection = target.CreateISpreadable();
        infection.AddToInfection(1);
        AddInfection(target, infection);
    }

    public ISpreadableDataHandler DetermineStateInfectionGlobal(List<ISpreadableDataHandler> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    public void AddInfection(ISpreadableDataHandler spreadableDataHandler, ISpreadable param)
    {
        if (param.Amount < 1) return;
        ISpreadable foundResult = spreadableDataHandler.FindExistingInstance(param);
        if (foundResult == null)
        {
            bool success = spreadableDataHandler.AddISpreadable(param);
            if (!success)
            {
                
            }
        }
        else
        {
            bool successs = spreadableDataHandler.AddAmountToISpreadable(foundResult, param.Amount);
            if (!successs)
            {
                
                long allowedAmount = spreadableDataHandler.Limit - spreadableDataHandler.GetActualInfectionsCount();
                spreadableDataHandler.AddAmountToISpreadable(foundResult, allowedAmount);
            }
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