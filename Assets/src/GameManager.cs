using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CoreGameSO _coreGame;
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
        foreach (SceneEnum sceneEnum in coreGameSO.SceneList)
        {
            SceneManager.LoadSceneAsync((int)sceneEnum, LoadSceneMode.Additive);
        }
    }
}
