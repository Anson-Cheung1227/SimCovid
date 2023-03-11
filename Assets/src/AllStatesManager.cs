using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AllStatesManager : MonoBehaviour
{
    private class Initialization : ILoadOperation
    {
        public string Name { get; set; }
        public float Operations { get; set; }
        public float DoneOperations { get; set; }
        public MonoBehaviour Operator { get; set; }
        public List<ILoadOperation> AllStatesOperationList { get; set; }
        public void Load()
        {
            GetStateLoadProgress();
        }
        private async Task GetStateLoadProgress()
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < AllStatesOperationList.Count; i++)
                {
                    while (AllStatesOperationList[i].DoneOperations != AllStatesOperationList[i].Operations)
                    {

                    }
                    DoneOperations++;
                }
            });
        }
    }
    public List<ILoadOperation> AllstateLoadOperations = new List<ILoadOperation>();
    // Start is called before the first frame update
    void Start()
    {
        Initialization initialization = new Initialization
        {
            Name = "Loading States Data",
            Operations = AllstateLoadOperations.Count,
            DoneOperations = 0,
            Operator = this,
            AllStatesOperationList = AllstateLoadOperations,
        };
        GameManager.Instance.LoadOperations.Add(initialization);
        initialization.Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
