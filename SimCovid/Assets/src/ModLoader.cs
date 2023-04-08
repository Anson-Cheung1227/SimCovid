using SimCovidAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class ModLoader : MonoBehaviour
{
#if UNITY_EDITOR
    private string _modDllPath = Directory.GetCurrentDirectory() + "/build/SimCovid_Data/Mods/";
#else
    private string _modDllPath = Directory.GetCurrentDirectory() + "/SimCovid_Data/Mods/";
#endif
    public static ModLoader Instance;
    public List<IMod> ModList = new List<IMod>();
    private void Awake()
    {
        Instance = this;
        foreach (string modFolder in Directory.GetDirectories(_modDllPath).Select(Path.GetFileName))
        {
            Assembly assembly = Assembly.LoadFile($"{_modDllPath}/{modFolder}/{modFolder}.dll");
            JObject jsonObject = JsonConvert.DeserializeObject<JObject>(File.ReadAllText($"{_modDllPath}/{modFolder}/settings.json"));
            string iModPath = (string)jsonObject["main"];
            Type type = assembly.GetType(iModPath);
            IMod mod = Activator.CreateInstance(type) as IMod;
            ModList.Add(mod);
        }
    }
    void Start()
    {
        foreach (IMod mod in ModList)
        {
            mod.OnLoadMod();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
