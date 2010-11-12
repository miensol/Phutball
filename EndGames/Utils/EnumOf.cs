using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EndGames.Utils
{
    public abstract class EnumOf<T>
    {
        public static IEnumerable<T> All()
        {
            return typeof (T).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(field => (T) field.GetValue(null));
        }
    }
}