using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
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
    public List<ILoadOperation> LoadOperations = new List<ILoadOperation>();
    public List<DataManager> DataManagerList;
    // Start is called before the first frame update
    private void Awake() 
    {
        Instance = this;
    }
    void Start()
    {
        LoadLevel(_coreGame);
    }
    public void LoadLevel(CoreGameSO coreGameSO)
    {
        _loadingScreen.SetActive(true);
        _scenesLoading.Clear();
        foreach (SceneEnum sceneEnum in coreGameSO.SceneList)
        {
            _scenesLoading.Add(SceneManager.LoadSceneAsync((int)sceneEnum, LoadSceneMode.Additive));
        }
        StartCoroutine(GetSceneLoadingProgress());
        StartCoroutine(UpdateLoadingVisuals());
    }
    private IEnumerator GetSceneLoadingProgress()
    {
        for (int i = 0; i < _scenesLoading.Count; ++i)
        {
            _sceneProgress = 0;
            foreach (AsyncOperation asyncOperation in _scenesLoading)
            {
                _sceneProgress += asyncOperation.progress;
            }
            _sceneProgress = _sceneProgress / _scenesLoading.Count;
            _progressBar.fillAmount = _sceneProgress;
            while (!_scenesLoading[i].isDone)
            {
                yield return null;
            }
        }
        GameEventManager.Instance.InvokeOnSetSceneId(currentSceneId);
        _sceneProgress = 1;
        ++currentSceneId;
        StartCoroutine(GetLoadingProgress());
    }
    private IEnumerator GetLoadingProgress()
    {
        for (; _currentOperation < LoadOperations.Count; ++_currentOperation)
        {
            while (!((LoadOperations[_currentOperation].DoneOperations / LoadOperations[_currentOperation].Operations) == 1))
            {
                _totalProgress = 0;
                foreach (ILoadOperation loadOperation in LoadOperations) 
                {
                    _totalProgress += loadOperation.DoneOperations / loadOperation.Operations;
                }
                _totalProgress /= LoadOperations.Count;
                yield return null;
            }
            foreach (ILoadOperation loadOperation in LoadOperations)
            {
                _totalProgress += loadOperation.DoneOperations / loadOperation.Operations;
            }
            _totalProgress /= LoadOperations.Count;
        }
        _totalProgress = 1;
        _loadingScreen.SetActive(false);
    }
    private IEnumerator UpdateLoadingVisuals()
    {
        while (_loadingScreen.activeInHierarchy)
        {
            _progressBar.fillAmount = (_sceneProgress + _totalProgress) / 2;
            if (_sceneProgress == 1)
            {
                _loadingTextProgress.text = LoadOperations[_currentOperation].Name + "... " +
                    (LoadOperations[_currentOperation].DoneOperations / LoadOperations[_currentOperation].Operations) * 100 + "%";
            }
            else
            {
                _loadingTextProgress.text = "Loading Environment... " +_sceneProgress * 100 + "%";
            }
            yield return null;
        }
        _progressBar.fillAmount = 1;
    }
}
