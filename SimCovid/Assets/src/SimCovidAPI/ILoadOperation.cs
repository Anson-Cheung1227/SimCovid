using UnityEngine;

namespace SimCovidAPI
{
    /// <summary>
    /// Represents a loading operation
    /// </summary>
    public interface ILoadOperation
    {
        public string Name { get; set; }
        public float Operations { get; set; }
        public float DoneOperations { get; set; }
        public MonoBehaviour Operator { get; set; }
        public abstract void Load();
    }
}
