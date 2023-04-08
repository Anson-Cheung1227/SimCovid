using UnityEngine;

//Class for holding a scene Id, should only be one instance per scene
namespace SimCovid.Core
{
    public class SceneIdHolder : MonoBehaviour
    {
        public int Id;
        void Awake()
        {
            if (GameEventManager.Instance != null) GameEventManager.Instance.OnSetSceneId += SetSceneId;
        }
        private void SetSceneId(int id)
        {
            GameEventManager.Instance.OnSetSceneId -= SetSceneId;
            Id = id;
        }
    }
}
