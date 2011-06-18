using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Phutball
{
    public class Worker<T>
    {
        private readonly IEnumerable<T> _args;

        public Worker(IEnumerable<T> args)
        {
            _args = args;
        }

        public void Do(Action<T> work)
        {
            _args.Each(work);
        }
    }

    public static class RubyExtensions
    {
        public static Worker<int> Upto(this int from, int to)
        {
            return new Worker<int>(Enumerable.Range(from, to));
        }
    }


    public static class BasicExtensions
    {
        private const string XmlHttpRequestValue = "XMLHttpRequest";
        public const string XRequestedWithHeader = "X-Requested-With";


        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> sequence)
        {
            return new HashSet<T>(sequence);
        }

        public static string ToMinutesAndSeconds(this TimeSpan ts)
        {
            return "{0}:{1}".ToFormat(ts.Minutes, ts.Seconds.ToString("00"));
        }

        public static bool IsEven(this int number)
        {
            return number%2 == 0;
        }


        public static IEnumerable<TValue> Enumerate<TValue>(this IEnumerator<TValue> enumerator)
        {
            using(enumerator)
            {
                while(enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
            }
        }

        public static TReturn FirstValue<TItem, TReturn>(this IEnumerable<TItem> enumerable, Func<TItem, TReturn> func)
            where TReturn : class
        {
            foreach (TItem item in enumerable)
            {
                TReturn @object = func(item);
                if (@object != null) return @object;
            }

            return null;
        }


        public static bool IsAjaxRequest(this IDictionary<string, object> requestInput)
        {
            object value;
            return
                requestInput.TryGetValue(XRequestedWithHeader, out value)
                && IsAjaxRequest(value);
        }


        private static bool IsAjaxRequest(object value)
        {
            return XmlHttpRequestValue.Equals(value as string, StringComparison.InvariantCultureIgnoreCase);
        }

        public static void Fill<T>(this IList<T> list, T value)
        {
            if (list.Contains(value)) return;
            list.Add(value);
        }

        public static string Join(this IEnumerable<string> strings, string separator)
        {
            string[] array = strings.ToArray();
            return string.Join(separator, array);
        }

        public static bool IsEmpty<T>(this IEnumerable<T> sequence)
        {
            return sequence.Any() == false;
        }        

        public static bool IsEmpty(this string stringValue)
        {
            return string.IsNullOrEmpty(stringValue);
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> sequence)
        {
            return sequence == null || sequence.IsEmpty();
        }

        public static bool IsNotEmpty(this string stringValue)
        {
            return !string.IsNullOrEmpty(stringValue);
        }

        public static bool ToBool(this string stringValue)
        {
            if (string.IsNullOrEmpty(stringValue)) return false;

            return bool.Parse(stringValue);
        }

        public static string ToFormat(this string stringFormat, params object[] args)
        {
            return String.Format(stringFormat, args);
        }

        public static string If(this string html, Expression<Func<bool>> modelBooleanValue)
        {
            return GetBooleanPropertyValue(modelBooleanValue) ? html : string.Empty;
        }

        public static string IfNot(this string html, Expression<Func<bool>> modelBooleanValue)
        {
            return !GetBooleanPropertyValue(modelBooleanValue) ? html : string.Empty;
        }

        private static bool GetBooleanPropertyValue(Expression<Func<bool>> modelBooleanValue)
        {
            var prop = modelBooleanValue.Body as MemberExpression;
            if (prop != null)
            {
                var info = prop.Member as PropertyInfo;
                if (info != null)
                {
                    return modelBooleanValue.Compile().Invoke();
                }
            }
            throw new ArgumentException(
                "The modelBooleanValue parameter should be a single property, validation logic is not allowed, only 'x => x.BooleanValue' usage is allowed, if more is needed do that in the Controller");
        }


        public static VALUE Get<KEY, VALUE>(this IDictionary<KEY, VALUE> dictionary, KEY key)
        {
            return dictionary.Get(key, default(VALUE));
        }

        public static VALUE Get<KEY, VALUE>(this IDictionary<KEY, VALUE> dictionary, KEY key, VALUE defaultValue)
        {
            if (dictionary.ContainsKey(key)) return dictionary[key];
            return defaultValue;
        }

        public static bool Exists<T>(this IEnumerable<T> values, Func<T, bool> evaluator)
        {
            return values.Count(evaluator) > 0;
        }

        [DebuggerStepThrough]
        public static IEnumerable<T> Each<T>(this IEnumerable<T> values, Action<T> eachAction)
        {
            foreach (T item in values)
            {
                eachAction(item);
            }

            return values;
        }

        [DebuggerStepThrough]
        public static IEnumerable Each(this IEnumerable values, Action<object> eachAction)
        {
            foreach (object item in values)
            {
                eachAction(item);
            }

            return values;
        }

        [DebuggerStepThrough]
        public static int IterateFromZero(this int maxCount, Action<int> eachAction)
        {
            for (int idx = 0; idx < maxCount; idx++)
            {
                eachAction(idx);
            }

            return maxCount;
        }

        public static ATTRIBUTE GetCustomAttribute<ATTRIBUTE>(this MemberInfo member)
            where ATTRIBUTE : Attribute
        {
            return member.GetCustomAttributes(typeof (ATTRIBUTE), false).FirstOrDefault() as ATTRIBUTE;
        }

        public static bool HasCustomAttribute<ATTRIBUTE>(this MemberInfo member)
            where ATTRIBUTE : Attribute
        {
            return member.GetCustomAttributes(typeof (ATTRIBUTE), false).Any();
        }

        public static bool IsNullableOfT(this Type theType)
        {
            return theType.IsGenericType && theType.GetGenericTypeDefinition().Equals(typeof (Nullable<>));
        }

        public static bool IsNullableOf(this Type theType, Type otherType)
        {
            return theType.IsNullableOfT() && theType.GetGenericArguments()[0].Equals(otherType);
        }

        public static bool IsTypeOrNullableOf<T>(this Type theType)
        {
            Type otherType = typeof (T);
            return theType == otherType ||
                   (theType.IsNullableOfT() && theType.GetGenericArguments()[0].Equals(otherType));
        }

        public static IList<T> AddMany<T>(this IList<T> list, params T[] items)
        {
            return list.AddRange(items);
        }

        public static IList<T> AddRange<T>(this IList<T> list, IEnumerable<T> items)
        {
            items.Each(list.Add);
            return list;
        }


        public static string UrlEncoded(this object target)
        {
            //properly encoding URI: http://blogs.msdn.com/yangxind/default.aspx
            return target != null ? Uri.EscapeDataString(target.ToString()) : string.Empty;
        }

        public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source,
          Func<TSource, TKey> selector)
        {
            return source.MaxBy(selector, Comparer<TKey>.Default);
        }


        /// <summary>
        /// Returns the maximal element of the given sequence, based on
        /// the given projection and the specified comparer for projected values. 
        /// </summary>
        /// <remarks>
        /// If more than one element has the maximal projected value, the first
        /// one encountered will be returned. This overload uses the default comparer
        /// for the projected type. This operator uses immediate execution, but
        /// only buffers a single result (the current maximal element).
        /// </remarks>
        /// <typeparam name="TSource">Type of the source sequence</typeparam>
        /// <typeparam name="TKey">Type of the projected element</typeparam>
        /// <param name="source">Source sequence</param>
        /// <param name="selector">Selector to use to pick the results to compare</param>
        /// <param name="comparer">Comparer to use to compare projected values</param>
        /// <returns>The maximal element, according to the projection.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/>, <paramref name="selector"/> 
        /// or <paramref name="comparer"/> is null</exception>
        /// <exception cref="InvalidOperationException"><paramref name="source"/> is empty</exception>

        public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> selector, IComparer<TKey> comparer)
        {
            using (IEnumerator<TSource> sourceIterator = source.GetEnumerator())
            {
                if (!sourceIterator.MoveNext())
                {
                    throw new InvalidOperationException("Sequence was empty");
                }
                TSource max = sourceIterator.Current;
                TKey maxKey = selector(max);
                while (sourceIterator.MoveNext())
                {
                    TSource candidate = sourceIterator.Current;
                    TKey candidateProjected = selector(candidate);
                    if (comparer.Compare(candidateProjected, maxKey) > 0)
                    {
                        max = candidate;
                        maxKey = candidateProjected;
                    }
                }
                return max;
            }
        }
    }
}