using System;
using System.Collections.Generic;
using System.Linq;

namespace SimCovidCore.Worlds
{
    public sealed class PresetWorld
    {
        public List<Country> _countries { get; set; } = new List<Country>();

        public PresetWorld()
        {
            
        }
        public World Build()
        {
            return new World(this);
        }
    }
    public sealed class World
    {
        private List<Country> _countries;
        internal World(PresetWorld presetWorld)
        {
            _countries = presetWorld._countries.ToList();
        }
    }
}