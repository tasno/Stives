using System;

namespace StIvesLib.Exceptions
{
    [Serializable]
    class TooManyWivesException : ApplicationException
    {
        public TooManyWivesException()
            : base("You cannot marry more than 7 wives.")
        {
        }
    }
}
