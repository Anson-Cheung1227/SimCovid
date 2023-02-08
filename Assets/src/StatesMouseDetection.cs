using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesMouseDetection : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private StateColorSO _stateColorSORef; 
    [SerializeField] private SpriteRenderer _spriteRenderer; 
    [SerializeField] private StateController _stateController; 
    [SerializeField] private GameObject _stateDetailsUIPanel;
    [SerializeField] private CameraController _cameraController;
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (DataManager.Instance.SelectedState == _stateController.State) _spriteRenderer.color = _stateColorSORef.SelectedColor;
        else
        {
            if (DataManager.Instance.HoveringState == _stateController.State) _spriteRenderer.color = _stateColorSORef.HoveringColor;
            else _spriteRenderer.color = _stateColorSORef.OriginalColor;
        }
    }
    private void OnMouseOver() 
    {
        if (_cameraController.IsPointerOverUI()) return;
        DataManager.Instance.HoveringState = _stateController.State;
    }
    private void OnMouseExit() 
    {
        DataManager.Instance.HoveringState = null;   
    }
    private void OnMouseDown() 
    {
        if (_cameraController.IsPointerOverUI()) return;
        DataManager.Instance.SelectedState = _stateController.State;
        DataManager.Instance.ActiveStateDetailsPanel = true;
    }
}
