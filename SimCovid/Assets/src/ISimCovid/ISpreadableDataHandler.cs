using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISimCovid
{
    public interface ISpreadableDataHandler
    {
        public long Count { get; }
        public IEnumerable<ISpreadable> GetAll();
        public void AddISpreadable(ISpreadable spreadable);
        public void RemoveISpreadable(ISpreadable spreadable);
    }
}
