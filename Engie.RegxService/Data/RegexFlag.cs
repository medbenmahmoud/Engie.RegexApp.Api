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
        #region .Net RegularExpressions enum
        None = 0,
        IgnoreCase = 1,
        Multiline = 2,
        ExplicitCapture = 4,
        Singleline = 16,
        IgnorePatternWhitespace = 32,
        RightToLeft = 64,
        #endregion

        Global = 5000,
    }

    public static class RegexFlag
    {
        public static List<int> GetRegexOptionsIds()
        {
            return Enum.GetValues(typeof(SpecificRegexOptions)).Cast<int>().ToList();
        }

        public static bool IsValidRegexOptionsId(int id)
        {
            return GetRegexOptionsIds().Any(x => x == id);
        }
    }
}
