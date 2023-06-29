namespace SimCovidAPI.Policies
{
    public interface IDraftable
    {
        public int DaysNeeded { get; }
        public void Draft();
    }
}