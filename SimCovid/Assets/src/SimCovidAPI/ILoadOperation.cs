using System.Threading.Tasks;
using UnityEngine;

namespace SimCovidAPI
{
    /// <summary>
    /// Represents a loading operation
    /// </summary>
    public interface ILoadOperation
    {
        public string Name { get; set; }
        public long Operations { get; set; }
        public long DoneOperations { get; set; }
        public MonoBehaviour Operator { get; set; }
        public Task Load();
    }
}
