using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Utilities
{
    public static class StringManipulation
    {
        public static string Neutralize(string text)
        {
            return Regex.Replace(text.Replace("'", ""), @"<(.|\n)*?>", string.Empty).Replace("'", "").Replace("&nbsp;", "").Replace(";", "").Replace(",", ".").Replace(")", "").Replace("\"", "");
        }
    }
}
