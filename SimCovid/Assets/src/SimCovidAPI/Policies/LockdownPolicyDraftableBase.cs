namespace SimCovidAPI.Policies
{
    public class LockdownPolicyDraftableBase : LockdownPolicyBase, IDraftable
    {
        public int DaysNeeded { get; } = 5;
        public void Draft()
        {
            
        }

        public void EnforceDraft()
        {
            throw new System.NotImplementedException();
        }
    }
}