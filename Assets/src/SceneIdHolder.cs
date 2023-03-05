using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneIdHolder : MonoBehaviour
{
    public int Id;
    // Start is called before the first frame update
    void Awake()
    {
        GameEventManager.Instance.OnSetSceneId += SetSceneId;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SetSceneId(int id)
    {
        GameEventManager.Instance.OnSetSceneId -= SetSceneId;
        Id = id;
    }
}
