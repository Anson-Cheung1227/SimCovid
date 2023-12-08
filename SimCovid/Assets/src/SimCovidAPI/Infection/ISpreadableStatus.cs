namespace SimCovidAPI.Infection
{
    public interface ISpreadableStatus
    {
        public string StatusName { get; }
        public string StatusTag { get; }
    }
}
