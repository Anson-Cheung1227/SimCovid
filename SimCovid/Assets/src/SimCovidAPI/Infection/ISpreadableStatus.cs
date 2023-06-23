namespace SimCovidAPI.Infection
{
    public interface ISpreadableStatus
    {
        public string StatusName { get; }
        public int StatusValue { get; }
    }
}
