using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISimCovid
{
    public interface IMod
    {
        public string Name { get; }
        public string Description { get; }
        public void OnLoadMod();
    }
    public class IModBase : IMod
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public void OnLoadMod() { }
    }
}
