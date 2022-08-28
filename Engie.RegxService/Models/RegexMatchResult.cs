using System.Collections.Generic;

namespace Engie.RegexApp.Api.Models
{
    public class RegexMatchResult
    {
        public RegexMatchResult()
        {
            MatchDetails = new List<MatchDetail>();
        }
        public bool IsMatch { get; set; }
        public string Message { get; internal set; }
        public string SubstitutionText { get; set; }
        public IList<MatchDetail> MatchDetails { get; set; }    
    }

    public class MatchDetail
    {
        public MatchDetail()
        {
            Groups = new List<RegexMatch>();
        }
        public RegexMatch FullMatch { get; set; }
        public List<RegexMatch> Groups { get; set; }
    }

    public class RegexMatch
    {
        public int Index { get; set; }
        public int Length { get; set; }
        public string value { get; set; }
    }
}
