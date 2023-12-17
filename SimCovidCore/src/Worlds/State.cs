namespace SimCovidCore.Worlds
{
    public sealed class State
    {
        public string Name { get; private set; }

        public State(string name)
        {
            Name = name;
        }
    }
}