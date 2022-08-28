using Engie.RegexApp.Api.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Engie.RegexApp.Api.Mappers
{
    public static class MatchDetailMapper
    {
        public static List<MatchDetail> ToMatchDetails(this MatchCollection matches)
        {
            var result = new List<MatchDetail>();
            foreach (Match match in matches)
            {
                result.Add(match.ToMatchDetails());
            };

            return result;
        }

        public static MatchDetail ToMatchDetails(this Match match)
        {
            var matchDetails = new MatchDetail();

            foreach (Group group in match.Groups.Values)
            {
                if (matchDetails.FullMatch == null)
                {
                    matchDetails.FullMatch = new RegexMatch()
                    {
                        Index = group.Index,
                        value = group.Value,
                        Length = group.Length
                    };
                }
                else
                {
                    matchDetails.Groups.Add(new RegexMatch()
                    {
                        Index = group.Index,
                        value = group.Value,
                        Length = group.Length
                    });

                }
            }

            return matchDetails;
        }
    }
    
}
