using System.Collections.Generic;
using Core;
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
        GenerateAllInfectionsLocal<Infection>(_allStates);
        GenerateAllInfectionsInterstate<Infection>(_allStates);
        GenerateAllInfectionsGlobal<Infection>(_allStates);
        UpdateInfectionList(dataManager.StateInfectionsTable, _allStates);
    }

    private void GenerateAllInfectionsLocal<TISpreadableTarget>(List<State> states)
        where TISpreadableTarget : class, ISpreadable, new()
    {
        foreach (State state in states)
        {
            if (!state.LocalLockdown)
            {
                GenerateInfectionsLocal(state.InfectionManager.GetActive());
            }
        }
    }

    private void GenerateAllInfectionsInterstate<TISpreadabeTarget>(List<State> states)
        where TISpreadabeTarget : class, ISpreadable, new()
    {
        List<ISpreadableDataHandler<TISpreadabeTarget>> eligibleStates =
            new List<ISpreadableDataHandler<TISpreadabeTarget>>();
        foreach (State state in states)
        {
            if (!state.InterstateLockdown)
            {
                eligibleStates.Add((ISpreadableDataHandler<TISpreadabeTarget>)state.InfectionManager.GetActive());
            }
        }

        GenerateInfectionsInterstate(eligibleStates);
    }

    public void GenerateInfectionsLocal<TISpreadableTarget>(
        ISpreadableDataHandler<TISpreadableTarget> activeSpreadableDataHandler)
        where TISpreadableTarget : class, ISpreadable, new()
    {
        var newInfection = activeSpreadableDataHandler.CreateISpreadable();
        newInfection.AddToInfection(activeSpreadableDataHandler.GetActualInfectionsCount());
        AddInfection(activeSpreadableDataHandler, newInfection);
    }

    public void GenerateInfectionsInterstate<TISpreadableTarget>(
        List<ISpreadableDataHandler<TISpreadableTarget>> activeSpreadableDataHandler)
        where TISpreadableTarget : class, ISpreadable, new()
    {
        /*
            We do not want new generated infections to pass on immediately, else we'll be in an infinite loop
            We will add all infections at once after the calculations
        */
        ISpreadableDataHandler<TISpreadableTarget> target;
        Dictionary<ISpreadableDataHandler<TISpreadableTarget>, long> delayedInfection =
            new Dictionary<ISpreadableDataHandler<TISpreadableTarget>, long>();
        foreach (ISpreadableDataHandler<TISpreadableTarget> spreadableDataHandler in activeSpreadableDataHandler)
        {
            target = DetermineTargetInfectionInterstate(activeSpreadableDataHandler);
            if (delayedInfection.ContainsKey(target))
                delayedInfection[target] += spreadableDataHandler.GetActualInfectionsCount();
            else
                delayedInfection.Add(target, spreadableDataHandler.GetActualInfectionsCount());
        }

        //Loop through the delayedInfection Dictionary, and add infections
        foreach (KeyValuePair<ISpreadableDataHandler<TISpreadableTarget>, long> infection in delayedInfection)
        {
            var newInfection = infection.Key.CreateISpreadable();
            newInfection.AddToInfection(infection.Value);
            AddInfection(infection.Key, newInfection);
        }
    }

    public ISpreadableDataHandler<ISpreadableTarget> DetermineTargetInfectionInterstate<ISpreadableTarget>(
        List<ISpreadableDataHandler<ISpreadableTarget>> list) where ISpreadableTarget : class, ISpreadable, new()
    {
        //TODO: Take in how likely the people are gonna travel
        return list[Random.Range(0, list.Count)];
    }

    private void GenerateAllInfectionsGlobal<TISpreadableTarget>(List<State> stateList)
        where TISpreadableTarget : class, ISpreadable, new()
    {
        List<ISpreadableDataHandler<TISpreadableTarget>> spreadableDataHandlers =
            new List<ISpreadableDataHandler<TISpreadableTarget>>();
        foreach (State state in stateList)
        {
            spreadableDataHandlers.Add((ISpreadableDataHandler<TISpreadableTarget>)state.InfectionManager.GetActive());
        }

        GenerateInfectionsGlobal(spreadableDataHandlers);
    }

    public void GenerateInfectionsGlobal<TISpreadableTarget>(List<ISpreadableDataHandler<TISpreadableTarget>> list)
        where TISpreadableTarget : class, ISpreadable, new()
    {
        ISpreadableDataHandler<TISpreadableTarget> target = DetermineStateInfectionGlobal(list);
        TISpreadableTarget infection = new TISpreadableTarget();
        infection.AddToInfection(1);
        AddInfection(target, infection);
    }

    public ISpreadableDataHandler<ISpreadableTarget> DetermineStateInfectionGlobal<ISpreadableTarget>(
        List<ISpreadableDataHandler<ISpreadableTarget>> list) where ISpreadableTarget : class, ISpreadable, new()
    {
        return list[Random.Range(0, list.Count)];
    }

    public void AddInfection<ISpreadableTarget>(ISpreadableDataHandler<ISpreadableTarget> spreadableDataHandler,
        ISpreadableTarget param) where ISpreadableTarget : class, ISpreadable, new()
    {
        if (param.Amount < 1) return;
        ISpreadableTarget foundResult = spreadableDataHandler.FindExistingInstance(param);
        if (foundResult == null)
        {
            bool success = spreadableDataHandler.AddISpreadable(param);
            if (!success)
            {
                param.AddToInfection(param.Amount * -1); //Reset
                //TODO: Reset
                spreadableDataHandler.AddISpreadable(param);
            }
        }
        else
        {
            foundResult.AddToInfection(param.Amount);
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