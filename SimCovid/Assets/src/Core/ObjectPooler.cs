using System.Collections.Generic;
using System.Threading.Tasks;
using SimCovid.Core.GameManagement;
using SimCovidAPI;
using UnityEngine;

namespace SimCovid.Core
{
    /// <summary>
    /// Pool class for handling Pools
    /// </summary>
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
    /// <summary>
    /// Object Pooler to handle initialization of Pools
    /// </summary>
    public class ObjectPooler : MonoBehaviour
    {
        public List<Pool> Pools;
        private class Initialization : ILoadOperation
        {
            public string Name { get; set; }
            public long Operations { get; set; }
            public long DoneOperations { get; set; }
            public MonoBehaviour Operator { get; set; }
            public List<Pool> Pools;
            public Task Load()
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
                    DoneOperations++;
                }

                return Task.CompletedTask;
            }
        }
        private void Awake()
        {
            Initialization initialization = new Initialization
            {
                Name = "Generating Objects",
                Operations = Pools.Count,
                DoneOperations = 0,
                Operator = this,
                Pools = this.Pools
            };
            GameManager.Instance.ResourceLoader.AddILoadOperation(initialization);
        }
    }
}