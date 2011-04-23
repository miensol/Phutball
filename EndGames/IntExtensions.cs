using System;
using System.Collections.Generic;

namespace Phutball
{
    public static class IntExtensions
    {
        public static IEnumerable<TValue> Times<TValue>(this int i, Func<TValue> work)
        {
            return Times(i, (idx) => work());
        }

        public static IEnumerable<TValue> Times<TValue>(this int i, Func<int,TValue> work)
        {
            for(var index = 0  ; index < i; ++index)
            {
                yield return work(index);
            }
        }
    }
}