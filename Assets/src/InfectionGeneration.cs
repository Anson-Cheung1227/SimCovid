using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionGeneration : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<State> _allState = new List<State>();
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    public void GenerateInfection()
    {
        ++_allState[Random.Range(0, _allState.Count - 1)].Infections;
    }
}
