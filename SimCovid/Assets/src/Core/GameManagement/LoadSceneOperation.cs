using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimCovidAPI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SimCovid.Core.GameManagement
{
    public partial class GameManager : MonoBehaviour
    {
        private class LoadSceneOperation : ILoadOperation
        {
            public string Name { get; set; } = "Loading Scenes";
            public long Operations { get; set; }
            public long DoneOperations { get; set; } = 0;
            public MonoBehaviour Operator { get; set; }
            public List<SceneEnum> SceneList;
            public List<AsyncOperation> ScenesLoading;
            public GameObject LoadingScreen;
            public async Task Load()
            {
                LoadingScreen.SetActive(true);
                ScenesLoading.Clear();
                foreach (SceneEnum sceneEnum in SceneList)
                {
                    ScenesLoading.Add(SceneManager.LoadSceneAsync((int)sceneEnum, LoadSceneMode.Additive));
                }

                Operator.StartCoroutine(LoadingProgress());
            }

            private IEnumerator LoadingProgress()
            {
                foreach (AsyncOperation checkAsyncOperation in ScenesLoading)
                {
                    while (!checkAsyncOperation.isDone)
                    {
                        DoneOperations = CountLoadedScenes(ScenesLoading);
                        yield return null;
                    }
                }
                //Scene Loading is finished
                DoneOperations = Operations;
            }
            private int CountLoadedScenes(List<AsyncOperation> sceneList)
            {
                int count = 0;
                foreach (AsyncOperation asyncOperation in sceneList)
                {
                    if (asyncOperation.isDone)
                    {
                        ++count;
                    }
                }
                return count;
            }
        }
    }
}