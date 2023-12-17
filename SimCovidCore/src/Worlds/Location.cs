namespace SimCovidCore.Worlds
{
    public sealed class Location
    {
        public string Name { get; private set; }

        public Location(string name)
        {
            Name = name;
        }
    }
}