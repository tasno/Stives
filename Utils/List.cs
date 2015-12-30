using System;
using System.Collections.Generic;

namespace Utils
{
    public static class List
    {
        private static readonly Random Random = new Random();
        public static void Shuffle<T>(this IList<T> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = Random.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        public static T Pop<T>(this IList<T> list)
        {
            var result = default(T);
            if (list.Count > 0)
            {
                result = list[0];
                list.RemoveAt(0);
            }
            return result;
        }
    }
}
