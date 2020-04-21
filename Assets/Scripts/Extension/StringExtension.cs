using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public static class StringExtension 
{
    /// <summary>
    /// return true if the every char in the comparablestring is in the string
    /// </summary>
    /// <param name="comparableString"></param>
    /// <returns></returns>
    public static bool containUnOrdered(this string stringToCompare, string comparableString)
    {
        for (int i = 0; i < comparableString.Length; i++)
            if (!stringToCompare.Contains(comparableString[i].ToString()))
                return false;

        return true;
    }
}