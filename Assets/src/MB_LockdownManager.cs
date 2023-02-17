using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockdownManager : MonoBehaviour
{
    [SerializeField] DataManager _dataManager;
    [SerializeField] GameObject _localLockdownButton;
    [SerializeField] GameObject _interstateLockdownButton;
    [SerializeField] GameObject _globalLockdownButton;
    public void OnLockdownUpdateButtonClick(GameObject button)
    {
        if (button == _localLockdownButton) _dataManager.SelectedState.LocalLockdown = !_dataManager.SelectedState.LocalLockdown;
        if (button == _interstateLockdownButton) _dataManager.SelectedState.InterstateLockdown = !_dataManager.SelectedState.InterstateLockdown;
        if (button == _globalLockdownButton) 
        {
            _dataManager.SelectedState.GlobalLockdown = !_dataManager.SelectedState.GlobalLockdown;
            _dataManager.SelectedState.DailyIncomingPeople = 0;
        }
    }
}
