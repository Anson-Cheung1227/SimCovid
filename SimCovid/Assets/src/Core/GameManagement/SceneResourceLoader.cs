using System.Collections.Generic;
using System.Threading.Tasks;
using SimCovidAPI;
using UnityEngine;

namespace SimCovid.Core.GameManagement
{
    public partial class GameManager : MonoBehaviour
    {
        private class SceneResourceLoader : ResourceLoader
        {
            public SceneResourceLoader(string name, long operations) : base(name, operations)
            {
            }

            private bool CheckDoneSceneLoading(ILoadOperation sceneOperation)
            {
                return sceneOperation.DoneOperations == sceneOperation.Operations;
            }

            public override async Task LoadAll()
            {
                //Should NOT have more than one task. 
                List<Task> tasks = new List<Task>();
                foreach (ILoadOperation loadOperation in OperationsList)
                {
                    loadOperation.Load();
                }

                Task task = Task.Run(() =>
                {
                    bool finishedLoading = false;
                    while (!finishedLoading)
                    {
                        foreach (ILoadOperation sceneLoadOperation in OperationsList)
                        {
                            finishedLoading = CheckDoneSceneLoading(sceneLoadOperation);
                        }
                        Debug.Log("Loading scenes:" + DoneOperations + "/" + Operations);
                    }

                    DoneOperations = Operations;
                    Debug.Log("Loading scenes:" + DoneOperations + "/" + Operations);
                    Debug.Log("Scene loading internal task Done");
                });
                await task;
            }

            public override Task LoadAllAsync() => LoadAll();
        }
    }
}