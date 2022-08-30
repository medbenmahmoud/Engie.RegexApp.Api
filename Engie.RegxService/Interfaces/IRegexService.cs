using Engie.RegexApp.Api.Models;
using System.Collections.Generic;

namespace Engie.RegexApp.Api.Interfaces
{
    public interface IRegexService
    {
        RegexMatchResult CheckRegexExpression(RegexRequest regexRequest);
        List<int> GetRegexFlagsIds();
    }
}
