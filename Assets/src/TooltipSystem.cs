using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem Instance;
    [SerializeField] private GameObject _tooltip;
    private void Awake() 
    {
        Instance = this;    
    }
    public static void Show(string content, string header = "")
    {
        Instance._tooltip.GetComponent<Tooltip>().SetText(content, header);
        Instance._tooltip.SetActive(true);
    }
    public static void Hide()
    {
        Instance._tooltip.SetActive(false);
    }
}
