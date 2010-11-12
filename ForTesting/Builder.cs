using System;
using System.Linq;
using System.Reflection;

namespace ForTesting
{
    public abstract class Builder<T>
    {
        public static implicit operator T(Builder<T> instance)
        {
            return instance.Build();
        }

        public abstract T Build();

        protected TBuilder NewBuilder<TBuilder>(Action<TBuilder> setThings)
            where TBuilder : Builder<T>
        {
            var context = typeof (TBuilder);
            var fiels = context.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).Select(fi=> fi.GetValue(this)).ToArray();
            var newbuilder = (TBuilder)Activator.CreateInstance(context, BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Instance,
                                                                null, fiels, null);
        
            setThings(newbuilder);
            return newbuilder;
        }
    }
}