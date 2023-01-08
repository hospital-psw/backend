namespace HospitalLibrary.Util
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class SearchParser
    {
        public static List<string> Parse(string input)
        {
            if (input == null)
            {
                input = "";
            }
            return Regex.Matches(input, @"(?<match>\w+)|\""(?<match>[\w\s]*)""")
                        .Cast<Match>()
                        .Select(p => p.Groups["match"].Value)
                        .ToList();
        }
    }
}
