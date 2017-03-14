using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Utilities
{
    /// <summary>
    /// Vinberg: Useful functions related to string manipulation
    /// </summary>
    public static class StringManipulation
    {
        /// <summary>
        /// Vinberg: Remove potentially harmful string data
        /// </summary>
        /// <param name="text">string data to be neutralized</param>
        /// <returns>string without tags and certain characters</returns>
        public static string Neutralize(string text)
        {
            return Regex.Replace(text.Replace("'", ""), @"<(.|\n)*?>", string.Empty).Replace("'", "").Replace("&nbsp;", "").Replace(";", "").Replace(",", ".").Replace(")", "").Replace("\"", "");
        }
    }
}
