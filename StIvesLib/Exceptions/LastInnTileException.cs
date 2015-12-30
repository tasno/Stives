using System;

namespace StIvesLib.Exceptions
{
    [Serializable]
    public class LastInnTileException : ApplicationException
    {
        public LastInnTileException()
            : base("The last Inn tile has been drawn. This is the last day.")
        { }
    }
}
