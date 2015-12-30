using System;

namespace StIves.Interfaces
{
    [Serializable]
    public class EventArgs<T> : EventArgs
    {
        public EventArgs(T data)
        {
            Data = data;
        }
        public readonly T Data;
    }
}
