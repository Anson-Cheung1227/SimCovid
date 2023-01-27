using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UnityErrorAvoid : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake() 
    {
        DebugManager.instance.enableRuntimeUI = false;   
    }
}
