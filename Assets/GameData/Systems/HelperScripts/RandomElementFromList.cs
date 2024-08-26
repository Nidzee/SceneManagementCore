using System.Collections.Generic;
using System;


public static class RandomElementFromList
{
    static Random random = new Random();

    public static T GetRandomElement<T>(List<T> list)
    {
        if (list == null)
        {
            return default(T);
        }

        if (list.Count == 0)
        {
            return default(T);
        }

        int randomIndex = random.Next(0, list.Count);
        return list[randomIndex];
    }

    public static Tuple<TKey, TValue> GetRandomKeyValue<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
    {
        if (dictionary == null || dictionary.Count == 0)
        {
            throw new ArgumentException("Dictionary is null or empty.");
        }

        // Get a list of keys
        List<TKey> keys = new List<TKey>(dictionary.Keys);

        // Generate a random index
        int randomIndex = UnityEngine.Random.Range(0, keys.Count);

        // Get the key at the random index
        TKey randomKey = keys[randomIndex];

        // Get the value associated with the random key
        TValue randomValue = dictionary[randomKey];

        // Return the key-value pair as a tuple
        return Tuple.Create(randomKey, randomValue);
    }
}