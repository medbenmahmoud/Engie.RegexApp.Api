using System.Collections.Generic;

namespace Engie.RegexApp.Api.Models
{
    public class RegexRequest
    {
        public RegexRequest()
        {
            FlagIds = new List<int>();
        }
        public string Pattern { get; set; }
        public string Text { get; set; }
        public IList<int> FlagIds { get; set; }
        public string Substitution { get; set; }
    }
}
