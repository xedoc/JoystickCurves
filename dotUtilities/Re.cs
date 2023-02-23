using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Globalization;

namespace dotUtilities
{
    public static class Re
    {
        public static string GetSubString(string input, string re, int index)
        {
            var match = Regex.Match(input, re, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            if (!match.Success)
                return null;

            if (match.Groups.Count <= index)
                return null;

            var result = match.Groups[index].Value;

            if (String.IsNullOrEmpty(result))
            {
                //Debug.Print(String.Format("RE: {0}. Result = NULL", re));
                return null;
            }


            //Debug.Print(String.Format("RE: {0}. Result = {1}", re, result));

            return result;

        }


    }
}
