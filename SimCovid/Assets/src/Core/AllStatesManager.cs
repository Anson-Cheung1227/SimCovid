using System.Collections.Generic;
using System.Threading.Tasks;
using SimCovid.Core.GameManagement;
using SimCovidAPI;
using UnityEngine;

namespace SimCovid.Core
{
    /// <summary>
    /// Handles all states 
    /// </summary>
    public class AllStatesManager : MonoBehaviour
    {
        private class Initialization : ILoadOperation
        {
            public string Name { get; set; }
            public long Operations { get; set; }
            public long DoneOperations { get; set; }
            public MonoBehaviour Operator { get; set; }
            public List<ILoadOperation> AllStatesOperationList { get; set; }
            public async Task Load()
            {
                await GetStateLoadProgress();
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
        void Awake()
        {
            Initialization initialization = new Initialization
            {
                Name = "Loading States Data",
                Operations = AllstateLoadOperations.Count,
                DoneOperations = 0,
                Operator = this,
                AllStatesOperationList = AllstateLoadOperations,
            };
            GameManager.Instance.ResourceLoader.AddILoadOperation(initialization);
        }
    }
}
