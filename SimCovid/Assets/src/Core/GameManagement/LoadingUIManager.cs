using System;
using System.Linq;
using SimCovid.Core.GameManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SimCovid.Core.GameManagement
{
    public class LoadingUIManager : MonoBehaviour
    {
        [SerializeField] private Image _progressBar;
        [SerializeField] private TextMeshProUGUI _loadingTextProgress;
        [SerializeField] private GameManager _gameManager;

        private void Update()
        {
            _progressBar.fillAmount =(float)
                (_gameManager.SceneLoader.DoneOperations + _gameManager.ResourceLoader.DoneOperations) /
                (_gameManager.SceneLoader.Operations + _gameManager.ResourceLoader.Operations);
            if (_gameManager.SceneLoader.Operations != _gameManager.SceneLoader.DoneOperations)
            {
                _loadingTextProgress.text = "Loading Scene " +
                                            _gameManager.SceneLoader.OperationsList.First().DoneOperations + "/" +
                                            _gameManager.SceneLoader.OperationsList.First().Operations;
            }
            else
            {
                _loadingTextProgress.text = _gameManager.ResourceLoader.OperationsList.First().Name + " " +
                                            _gameManager.ResourceLoader.OperationsList.First().DoneOperations + "/" +
                                            _gameManager.ResourceLoader.OperationsList.First().Operations;
            }
        }
    }
}