using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneIdHolder : MonoBehaviour
{
    // Start is called before the first frame update
    public int Id;
    void Start()
    {
        GameEventManager.Instance.OnSetSceneId += SetSceneId;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SetSceneId(int id)
    {
        Id = id;
    }
}
