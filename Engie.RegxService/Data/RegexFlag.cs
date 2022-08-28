using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Engie.RegexApp.Api.Data
{

    /// <summary>
    /// Additional RegexOptions
    /// </summary>
    public enum SpecificRegexOptions
    {
        Global = 5000
    }

    public static class RegexFlag
    {
        public static List<int> GetRegexOptionsIds()
        {
            var flags = Enum.GetValues(typeof(RegexOptions)).Cast<int>().ToList();
            flags.AddRange(Enum.GetValues(typeof(SpecificRegexOptions)).Cast<int>().ToList());
            return flags;
        }

        public static bool IsValidRegexOptionsId(int id)
        {
            return GetRegexOptionsIds().Any(x => x == id);
        }
    }
}
