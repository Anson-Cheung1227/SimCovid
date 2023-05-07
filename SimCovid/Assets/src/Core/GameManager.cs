using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimCovidAPI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SimCovid.Core
{
    public class GameManager : MonoBehaviour
    {
        private class SceneResourceLoader : ResourceLoader
        {
            public SceneResourceLoader(string name, long operations) : base(name, operations)
            {
            }

            public override async Task LoadAll()
            {
                foreach (ILoadOperation loadOperation in OperationsList)
                {
                    loadOperation.Load();
                }

                Task task = Task.Run(() =>
                {
                    bool finishedLoading = false;
                    while (!finishedLoading)
                    {
                        finishedLoading = true;
                        foreach (ILoadOperation sceneLoadOperation in OperationsList)
                        {
                            if (sceneLoadOperation.DoneOperations != sceneLoadOperation.Operations)
                            {
                                finishedLoading = false;
                            }
                        }
                    }
                    Debug.Log("done");
                });
                await task;
                Debug.Log(false);
            }

            public override Task LoadAllAsync() => LoadAll();
        }

        private class InitializationResourceLoader : ResourceLoader
        {
            public InitializationResourceLoader(string name, long operations) : base(name, operations)
            {
            }
        }

        private class LoadSceneOperation : ILoadOperation
        {
            public string Name { get; set; } = "Loading Scenes";
            public long Operations { get; set; }
            public long DoneOperations { get; set; } = 0;
            public MonoBehaviour Operator { get; set; }
            public List<SceneEnum> SceneList;
            public List<AsyncOperation> ScenesLoading;
            public GameObject LoadingScreen;
            public Task Load()
            {
                LoadingScreen.SetActive(true);
                ScenesLoading.Clear();
                foreach (SceneEnum sceneEnum in SceneList)
                {
                    ScenesLoading.Add(SceneManager.LoadSceneAsync((int)sceneEnum, LoadSceneMode.Additive));
                }

                Operator.StartCoroutine(LoadingProgress());
                return Task.CompletedTask;
            }

            private IEnumerator LoadingProgress()
            {
                foreach (AsyncOperation checkAsyncOperation in ScenesLoading)
                {
                    while (!checkAsyncOperation.isDone)
                    {
                        long doneOperations = 0;
                        foreach (AsyncOperation asyncOperation in ScenesLoading)
                        {
                            if (asyncOperation.isDone)
                            {
                                ++doneOperations;
                            }
                        }

                        DoneOperations = doneOperations;
                        yield return null;
                    }
                }

                DoneOperations = Operations;
            }
        }
        [SerializeField] private CoreGameSO _coreGame;
        [SerializeField] private GameObject _loadingScreen;
        [SerializeField] private Image _progressBar;
        [SerializeField] private TextMeshProUGUI _loadingTextProgress;
        private List<AsyncOperation> _scenesLoading = new List<AsyncOperation>();
        private int _currentOperation;
        private float _totalProgress = 0;
        private float _sceneProgress = 0;
        private int currentSceneId = 0;
        public static GameManager Instance;
        private ResourceLoader SceneLoader = new SceneResourceLoader("Scene loader", 0);
        public ResourceLoader ResourceLoader = new InitializationResourceLoader("Resource Loader", 0);
        public List<DataManager> DataManagerList;
        // Start is called before the first frame update
        private void Awake()
        {
            Instance = this;
        }
        async void Start()
        {
            await LoadLevel(_coreGame);
        }
        public async Task LoadLevel(CoreGameSO coreGameSO)
        {
            LoadSceneOperation sceneOperation = new LoadSceneOperation
            {
                Operations = coreGameSO.SceneList.Count,
                Operator = this,
                SceneList = coreGameSO.SceneList,
                ScenesLoading = _scenesLoading,
                LoadingScreen = _loadingScreen,
            };
            SceneLoader.AddILoadOperation(sceneOperation);
            SceneLoader.SetOperations();
            Task a = SceneLoader.LoadAll();
            await a;
            Debug.Log("R Done");
            ResourceLoader.SetOperations();
            await ResourceLoader.LoadAllAsync();
        }
    }
}
