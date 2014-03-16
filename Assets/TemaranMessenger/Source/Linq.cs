// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Created by Johan Olofsson
// Last changed: 2014-03-15
// You're free to do whatever you want with the code except claiming it's you who wrote it.
// I would also appreciate it if you kept this file header as a thank you for the code :)

using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Since LINQ doesn't work on apple platforms, this is used to replace it
/// </summary>
public static class LinqExtensions
{
    public static void Apply<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
        foreach (var item in enumerable)
        {
            action(item);
        }
    }

    public static IEnumerable<TOut> Select<TIn, TOut>(this IEnumerable<TIn> self, Func<TIn, TOut> selector)
    {
        var result = new List<TOut>();
        foreach (var value in self)
            result.Add(selector(value));

        return Prettify(result);
    }

    public static IEnumerable<TValue> Where<TValue>(this IEnumerable<TValue> self, Predicate<TValue> condition)
    {
        var result = new List<TValue>();
        foreach (var value in self)
            if (condition(value))
                result.Add(value);

        return Prettify(result);
    }

    public static bool Any(this IEnumerable self)
    {
        var list = self as IList;
        if (list != null)
            return list.Count > 0;

        return self.GetEnumerator().MoveNext();
    }

    public static bool Any<TValue>(this IEnumerable<TValue> self, Predicate<TValue> condition)
    {
        foreach (var value in self)
            if (condition(value))
                return true;

        return false;
    }

    public static bool IsEmpty<TValue>(this IEnumerable<TValue> self)
    {
        return !self.GetEnumerator().MoveNext();
    }

    public static TValue FirstOrDefault<TValue>(this IEnumerable<TValue> self)
    {
        var enumerator = self.GetEnumerator();
        if (enumerator.MoveNext())
            return enumerator.Current;

        return default(TValue);
    }

    public static TValue FirstOrDefault<TValue>(this IEnumerable<TValue> self, Predicate<TValue> condition)
    {
        foreach (var value in self)
            if (condition(value))
                return value;

        return default(TValue);
    }

    public static TValue First<TValue>(this IEnumerable<TValue> self)
    {
        var enumerator = self.GetEnumerator();
        if (enumerator.MoveNext())
            return enumerator.Current;

        throw new Exception("No matching elements in collections");
    }

    public static TValue First<TValue>(this IEnumerable<TValue> self, Predicate<TValue> condition)
    {
        foreach (var value in self)
            if (condition(value))
                return value;

        throw new Exception("No matching elements in collections");
    }

    public static TValue LastOrDefault<TValue>(this IEnumerable<TValue> self)
    {
        var list = self as IList<TValue>;
        if (list == null)
            list = self.ToArray();

        if (list.Count != 0)
            return list[list.Count - 1];

        return default(TValue);
    }

    public static TValue Last<TValue>(this IEnumerable<TValue> self)
    {
        var list = self as IList<TValue>;
        if (list == null)
            list = self.ToArray();

        if (list.Count != 0)
            return list[list.Count - 1];

        throw new Exception("No elements in collections");
    }

    public static List<TValue> ToList<TValue>(this IEnumerable<TValue> self)
    {
        var result = new List<TValue>();
        foreach (var value in self)
            result.Add(value);

        return result;
    }

    public static TValue[] ToArray<TValue>(this IEnumerable<TValue> self)
    {
        return self.ToList().ToArray();
    }

    public static int Count(this IEnumerable self)
    {
        int count = 0;
        var enumerator = self.GetEnumerator();
        while (enumerator.MoveNext())
            count++;

        return count;
    }

    public static IEnumerable<TOut> Cast<TOut>(this IEnumerable self)
    {
        var result = new List<TOut>();
        foreach (var value in self)
            result.Add((TOut)value);

        return Prettify(result);
    }

    public static IEnumerable<TOut> SelectMany<TOut, TIn>(this IEnumerable<TIn> self, Func<TIn, IEnumerable<TOut>> selector)
    {
        var result = new List<TOut>();
        foreach (var value in self)
            result.AddRange(selector(value));

        return Prettify(result);
    }

    public static IEnumerable<TValue> Distinct<TValue>(this IEnumerable<TValue> self, Func<TValue, TValue, bool> equals)
    {
        var result = new List<TValue>();
        foreach (var value in self)
            if (!result.Any(uniqueValue => equals(uniqueValue, value)))
                result.Add(value);

        return Prettify(result);
    }

    public static IEnumerable<TValue> Distinct<TValue>(this IEnumerable<TValue> self, IEqualityComparer<TValue> comparer)
    {
        return self.Distinct((v0, v1) => comparer.Equals(v0, v1));
    }

    //public static IEnumerable<TValue> Distinct<TValue>(this IEnumerable<TValue> self)
    //{
    //    return self.Distinct((v0, v1) => v0.SafeEquals(v1));
    //}

    public static IEnumerable<TValue> Union<TValue>(this IEnumerable<TValue> self, IEnumerable<TValue> other)
    {
        var result = new List<TValue>();
        result.AddRange(self);
        result.AddRange(other);
        return Prettify(result);
    }

    public static System.Linq.IOrderedEnumerable<TValue> OrderBy<TValue, TKey>(this IEnumerable<TValue> self, Func<TValue, TKey> keySelector)
    {
        return System.Linq.Enumerable.OrderBy(self, keySelector);
    }

    public static System.Linq.IOrderedEnumerable<TValue> OrderByDescending<TValue, TKey>(this IEnumerable<TValue> self, Func<TValue, TKey> keySelector)
    {
        return System.Linq.Enumerable.OrderByDescending(self, keySelector);
    }

    public static System.Linq.IOrderedEnumerable<TValue> ThenBy<TValue, TKey>(this System.Linq.IOrderedEnumerable<TValue> self, Func<TValue, TKey> keySelector)
    {
        return System.Linq.Enumerable.ThenBy(self, keySelector);
    }

    public static System.Linq.IOrderedEnumerable<TValue> ThenByDescending<TValue, TKey>(this System.Linq.IOrderedEnumerable<TValue> self, Func<TValue, TKey> keySelector)
    {
        return System.Linq.Enumerable.ThenByDescending(self, keySelector);
    }

    public static IEnumerable<TValue> ToSorted<TValue>(this IEnumerable<TValue> self, Comparison<TValue> comparison)
    {
        var list = self.ToList();
        list.Sort(comparison);
        return list;
    }

    public static IEnumerable<TValue> ToSorted<TValue>(this IEnumerable<TValue> self, Func<TValue, TValue, bool> isLessThan)
    {
        return self.ToSorted((v0, v1) =>
        {
            if (isLessThan(v0, v1))
                return -1;
            if (isLessThan(v1, v0))
                return 1;
            return 0;
        });
    }

    public static TValue Aggregate<TValue>(this IEnumerable<TValue> self, Func<TValue, TValue, TValue> aggregator)
    {
        return System.Linq.Enumerable.Aggregate(self, aggregator);
    }

    public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<TValue> self, Func<TValue, TKey> getKey)
    {
        var dictionary = new Dictionary<TKey, TValue>();

        foreach (var value in self)
        {
            var key = getKey(value);
            dictionary.Add(key, value);
        }

        return dictionary;
    }

    #region Implementation

    private static IEnumerable<TValue> Prettify<TValue>(IEnumerable<TValue> values)
    {
#if DEBUG
        return values.ToArray();
#else
            return values;
#endif // DEBUG
    }

    #endregion
}