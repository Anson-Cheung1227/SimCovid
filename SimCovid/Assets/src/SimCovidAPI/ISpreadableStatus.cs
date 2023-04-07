using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace SimCovidAPI
{
    public interface ISpreadableStatus
    {
        public string StatusName { get; }
        public int StatusValue { get; }
    }
}
