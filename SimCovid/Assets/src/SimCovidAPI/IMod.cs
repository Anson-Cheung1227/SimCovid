using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimCovidAPI
{
    public interface IMod
    {
        public string Name { get; }
        public string Description { get; }
        public void OnLoadMod();
    }
}
