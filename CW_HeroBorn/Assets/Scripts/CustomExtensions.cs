//if a file uses Custom Extensions and doesn't have these does the program explode?
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// creates a namespace for us to use in other files
namespace CustomExtensions
{
    // organizational class
    public static class StringExtensions
    {
        // the this string str makes this an extention of the string class
        public static void FancyDebug(this string str)
        {
            Debug.LogFormat("This string contains {0} characters.",
            str.Length);
        }
    }
}
