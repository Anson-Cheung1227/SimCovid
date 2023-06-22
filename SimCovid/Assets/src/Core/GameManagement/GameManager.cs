using System.Collections.Generic;
using System.Threading.Tasks;
using SimCovidAPI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SimCovid.Core.GameManagement
{
    public partial class GameManager : MonoBehaviour
    {
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
            await SceneLoader.LoadAll();
            Debug.Log("Scene Loading Done");
            ResourceLoader.SetOperations();
            await ResourceLoader.LoadAllAsync();
            Debug.Log("Resource Loading Done");
            _loadingScreen.SetActive(false);
        }
    }
}
