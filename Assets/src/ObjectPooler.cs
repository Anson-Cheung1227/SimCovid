using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public GameObject Location; 
    public GameObject Prefab;
    public List<GameObject> PoolObjects;
    public int Size;
    public GameObject GetPooledObject()
    {
        foreach (GameObject gameObject in PoolObjects)
        {
            if (!gameObject.activeInHierarchy)
            {
                gameObject.SetActive(true);
                return gameObject;
            }
        }
        return null;
    }
}
public class ObjectPooler : MonoBehaviour
{
    public List<Pool> Pools;
    // Use this for initialization
    void Start()
    {
        foreach (Pool pool in Pools)
        {
            for (int i = 0; i < pool.Size; i++)
            {
                GameObject poolObject = Instantiate(pool.Prefab);
                poolObject.transform.SetParent(pool.Location.transform, false);
                Vector3 position = new Vector3(0, 0, 0);
                poolObject.GetComponent<RectTransform>().anchoredPosition = position;
                poolObject.SetActive(false);
                pool.PoolObjects.Add(poolObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}