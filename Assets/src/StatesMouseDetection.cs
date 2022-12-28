using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesMouseDetection : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private SpriteRenderer _spriteRenderer; 
    [SerializeField] private Color _originalColor;
    [SerializeField] private Color _hoveringColor;  
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    private void OnMouseOver() 
    {
        _spriteRenderer.color = _hoveringColor;
    }
    private void OnMouseExit() 
    {
        _spriteRenderer.color = _originalColor;    
    }
}
