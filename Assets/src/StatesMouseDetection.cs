using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesMouseDetection : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private DataManager _dataManager;
    [SerializeField] private StateColorSO _stateColorSORef; 
    [SerializeField] private SpriteRenderer _spriteRenderer; 
    [SerializeField] private StateController _stateController; 
    [SerializeField] private CameraController _cameraController;
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (_dataManager.SelectedState == _stateController.State) _spriteRenderer.color = _stateColorSORef.SelectedColor;
        else
        {
            if (_dataManager.HoveringState == _stateController.State) _spriteRenderer.color = _stateColorSORef.HoveringColor;
            else _spriteRenderer.color = _stateColorSORef.OriginalColor;
        }
    }
    private void OnMouseOver() 
    {
        if (_cameraController.IsPointerOverUI()) return;
        _dataManager.HoveringState = _stateController.State;
        TooltipSystem.Show("", _dataManager.HoveringState.Name);
    }
    private void OnMouseExit() 
    {
        _dataManager.HoveringState = null;
        TooltipSystem.Hide();
    }
    private void OnMouseDown() 
    {
        if (_cameraController.IsPointerOverUI()) return;
        _dataManager.SelectedState = _stateController.State;
        _dataManager.ActiveStateDetailsPanel = true;
    }
}
