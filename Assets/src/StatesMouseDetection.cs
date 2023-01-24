using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesMouseDetection : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private StateColorSO _stateColorSORef; 
    [SerializeField] private SpriteRenderer _spriteRenderer; 
    [SerializeField] private DataManager _dataManager; 
    [SerializeField] private StateController _stateController; 
    private bool isHovering; 
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (_dataManager.SelectedState == _stateController.State) _spriteRenderer.color = _stateColorSORef.SelectedColor;
        else
        {
            if (isHovering) _spriteRenderer.color = _stateColorSORef.HoveringColor;
            else _spriteRenderer.color = _stateColorSORef.OriginalColor;
        }
    }
    private void OnMouseOver() 
    {
        isHovering = true;
    }
    private void OnMouseExit() 
    {
        isHovering = false;   
    }
    private void OnMouseDown() 
    {
        _dataManager.SelectedState = _stateController.State;
    }
}
