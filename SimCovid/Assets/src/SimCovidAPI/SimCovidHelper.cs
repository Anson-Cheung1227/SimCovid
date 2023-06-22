using System;

namespace SimCovidAPI
{
    public class SimCovidHelper
    {
        private static Random _random;
        public static ISpreadable CreateISpreadableWithAmount(ISpreadableDataHandler spreadableDataHandler,long amount)
        {
            ISpreadable target = spreadableDataHandler.CreateISpreadable();
            target.AddToInfection(amount);
            return target;
        }
        public static bool AddISpreadable(ISpreadableDataHandler spreadableDataHandler, ISpreadable param)
        {
            bool success;
            ISpreadable findResult = spreadableDataHandler.FindExistingInstance(param);
            if (findResult == null)
            {
                success = spreadableDataHandler.AddISpreadable(param);
            }
            else
            {
                success = spreadableDataHandler.AddAmountToISpreadable(findResult, param.Amount);
            }

            return success;
        }
        public static bool CheckIfOverload(ISpreadableDataHandler spreadableDataHandler, ISpreadable param)
        {
            return spreadableDataHandler.GetActualISpreadablesCount() >= spreadableDataHandler.Limit;
        }

        public static bool BoolFromChance(int chance)
        {
            return _random.Next(0, 100) <= chance;
        }
    }
}