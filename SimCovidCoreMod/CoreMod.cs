using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISimCovid;
using UnityEngine;

public class CoreMod : IMod
{
    public string Name { get { return "SimCovid Core Mod"; } }
    public string Description { get { return "Test Mod"; } }
    public CoreMod()
    { }
    public void OnLoadMod()
    {
        Debug.Log("Hello from " + Name);
    }
}
