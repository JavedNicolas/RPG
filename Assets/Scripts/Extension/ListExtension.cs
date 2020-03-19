using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ListExtension
{
    public static T getRandomElement<T>(this List<T> list)
    {
        if (list.Count == 0)
            return default;

        int randomValue = Random.Range(0, list.Count);
        return list[randomValue];
    }

    public static List<T> join<T>(this List<T> list, params List<T>[] lists)
    {
        foreach (List<T> listToAdd in lists)
        {
            list.AddRange(listToAdd);
        }

        return list;
    }
}
