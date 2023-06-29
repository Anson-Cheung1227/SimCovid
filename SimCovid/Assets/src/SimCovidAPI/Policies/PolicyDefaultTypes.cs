namespace SimCovidAPI.Policies
{
    public class PolicyDefaultTypes
    {
        public static LockdownPolicyType Lockdown = new LockdownPolicyType();
        public class LockdownPolicyType : IPolicyType
        {
            ///<inheritdoc />
            public string PolicyName { get { return "Lockdown"; } }
            public string PolicyDescription { get { return "Citizens are not allowed to move locally, unless emergencies"; } }

            public static implicit operator string(LockdownPolicyType lockdownPolicyType) =>
                lockdownPolicyType.PolicyName;
        }
    }
}