using System;

namespace StIvesLib.Exceptions
{
    public class OutOfMoneyException : ApplicationException
    {
        public OutOfMoneyException(int attemptedSpend, int availableMoney, string playerName)
            : base(string.Format("{0} attempted to spend {1}d but only has {2}d available", playerName, attemptedSpend, availableMoney))
        { }
    }
}
