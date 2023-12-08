using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace SimCovid.Core.GameManagement
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private CoreGameSO _coreGameSO;
        public async void Play()
        {
            SceneManager.UnloadSceneAsync((int)SceneEnum.StartMenu);
            await GameManager.Instance.LoadLevel(_coreGameSO);
        }

        public void Quit()
        {
            Application.Quit();
        }

    }
}
