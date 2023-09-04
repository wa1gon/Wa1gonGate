using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdifLib
{
    public class QsoTracking
    {
        public static bool DetectAppFieldName(string input, string pattern = "guid")
        {
            // Define a regular expression pattern with a wildcard (_) using ".".
            // Use RegexOptions.IgnoreCase to make the pattern case-insensitive.
            string defaultPattern = $@"^app_.*_{pattern}$";
            Regex regex = new Regex(defaultPattern, RegexOptions.IgnoreCase);

            // Check if the input matches the pattern.
            return regex.IsMatch(input);
        }
        public static string ExtractFieldName(string input, string pattern = "guid")
        {
            // Use a regular expression to match the specified pattern and capture it.
            string regexPattern = $@"^.*({pattern}).*$";
            Match match = Regex.Match(input, regexPattern, RegexOptions.IgnoreCase);

            // If a match is found, return the captured pattern.
            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            // If no match is found, return an empty string.
            return string.Empty;
        }
    }
}
