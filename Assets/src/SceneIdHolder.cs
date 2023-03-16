using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class for holding a scene Id, should only be one instance per scene
public class SceneIdHolder : MonoBehaviour
{
    public int Id;
    void Awake()
    {
        GameEventManager.Instance.OnSetSceneId += SetSceneId;
    }
    private void SetSceneId(int id)
    {
        GameEventManager.Instance.OnSetSceneId -= SetSceneId;
        Id = id;
    }
}
