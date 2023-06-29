namespace SimCovidAPI.Policies
{
    public abstract class DraftablePolicyBase : PolicyBase, IDraftable
    {
        public DraftablePolicyBase(IPolicyType policyType, bool active = false) : base(policyType, active)
        {
        }

        public int DaysNeeded { get; }

        public void Draft()
        {
            SetActive(true);
        }
    }
}