using System.Collections.Generic;

namespace SimCovidAPI.Infection
{
    public interface ISpreadableManager
    {
        public long Limit { get; }
        public IEnumerable<ISpreadableDataHandler> GetAll();
        /// <summary>
        /// This methods assumes that the inherited member to use a Dictionary, accessed via keys.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public ISpreadableDataHandler GetISpreadableDataHandler(string tag);
        public void UpdateLimit();
        public long GetTotalISpreadableCount();
    }
}
