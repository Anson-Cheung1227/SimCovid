using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SimCovid
{
    public class MainMenu : MonoBehaviour
    {
        //[SerializeField] List<Scene> unloadedScenes = new List<Scene>();

        private void Start()
        {
        }
        public void Play()
        {
            SceneManager.LoadScene(1);
            //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        }

        public void Quit()
        {
            Application.Quit();
        }

    }
}
