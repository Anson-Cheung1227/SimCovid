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
        /// <param name="key"></param>
        /// <returns></returns>
        public ISpreadableDataHandler GetISpreadableDataHandler(int key);
        public void UpdateLimit();
        public long GetTotalISpreadableCount();
    }
}
