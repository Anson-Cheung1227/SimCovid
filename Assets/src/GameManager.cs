using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CoreGameSO _coreGame;
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Image _progressBar;
    private List<AsyncOperation> _scenesLoading = new List<AsyncOperation>();
    private float totalProgress = 0;
    public static GameManager Instance; 
    // Start is called before the first frame update
    private void Awake() 
    {
        Instance = this;    
    }
    void Start()
    {
        LoadLevel(_coreGame);
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }
    public IEnumerator GetSceneLoadingProgress()
    {
        for (int i = 0; i < _scenesLoading.Count; ++i)
        {
            totalProgress = 0;
            foreach (AsyncOperation asyncOperation in _scenesLoading)
            {
                totalProgress += asyncOperation.progress;
                Debug.Log(asyncOperation.progress);
            }
            totalProgress = (totalProgress / _scenesLoading.Count);
            _progressBar.fillAmount = totalProgress;
            while (!_scenesLoading[i].isDone)
            {
                yield return null;
            }
        }
        totalProgress = 1;
        _progressBar.fillAmount = totalProgress;
        _loadingScreen.SetActive(false);
    }
}
