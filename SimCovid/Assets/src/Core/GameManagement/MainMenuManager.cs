using UnityEngine;
using UnityEngine.SceneManagement;

namespace SimCovid.Core.GameManagement
{
    public class MainMenuManager : MonoBehaviour
    {
        public void Play()
        {
            SceneManager.LoadScene(1);
        }

        public void Quit()
        {
            Application.Quit();
        }

    }
}
