using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace SimCovid.Core.GameManagement
{
    public class MainMenuManager : MonoBehaviour
    {
        public void Play()
        {
            SceneManager.LoadSceneAsync((int)SceneEnum.GlobalScene, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync((int)SceneEnum.StartMenu);
        }

        public void Quit()
        {
            Application.Quit();
        }

    }
}
