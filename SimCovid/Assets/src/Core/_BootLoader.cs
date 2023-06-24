using UnityEngine;
using UnityEngine.SceneManagement;

namespace SimCovid.Core
{
    public class _BootLoader : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            SceneManager.LoadSceneAsync((int)SceneEnum.GlobalScene, LoadSceneMode.Single);
            SceneManager.LoadSceneAsync((int)SceneEnum.StartMenu, LoadSceneMode.Additive);
        }
    }
}
