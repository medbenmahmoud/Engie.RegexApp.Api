using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Engie.RegexApp.Api.Interfaces;
using Engie.RegexApp.Api.Models;
using Engie.RegexApp.Api.Mappers;
using Engie.RegexApp.Api.Data;

namespace Engie.RegexApp.Api.Services
{
    public sealed class RegexService : IRegexService
    {
        private static readonly Lazy<RegexService> _instance =
            new Lazy<RegexService>(() => new RegexService());

        private RegexService(){}

        public static RegexService Instance => _instance.Value;

        public RegexMatchResult CheckRegexExpression(RegexRequest regexRequest)
        {
            Regex rgx;
            if (regexRequest.FlagIds != null && regexRequest.FlagIds.Any())
            {
                var aggregatedOption = AggregateFlagsIds(regexRequest.FlagIds);
                rgx = new Regex(regexRequest.Pattern, aggregatedOption);
            }
            else
            {
                rgx = new Regex(regexRequest.Pattern);
            }

            var result = new RegexMatchResult();
            result.IsMatch = rgx.IsMatch(regexRequest.Text);

            if (regexRequest.FlagIds != null && regexRequest.FlagIds.Any(x => x == (int)SpecificRegexOptions.Global))
            {
                result.MatchDetails = rgx.Matches(regexRequest.Text)?.ToMatchDetails();
            }
            else
            {
                result.MatchDetails = new List<MatchDetail> { rgx.Match(regexRequest.Text)?.ToMatchDetails() };
            }

            if (!string.IsNullOrEmpty(regexRequest.Substitution))
            {
                result.SubstitutionText = rgx.Replace(regexRequest.Text, regexRequest.Substitution);
            }

            return result;
        }

        public List<int> GetRegexFlagsIds()
        {
            return RegexFlag.GetRegexOptionsIds();
        }

        private RegexOptions AggregateFlagsIds(IList<int> flagIds)
        {
            List<RegexOptions> options = new List<RegexOptions>();

            foreach (var flagId in flagIds)
            {
                if (Enum.TryParse(flagId.ToString(), out RegexOptions flag) && Enum.IsDefined(typeof(RegexOptions), flag))
                {
                    options.Add(flag);
                }
            }
            return options.Any() ? options.Aggregate((x, y) => x |= y) : RegexOptions.None;
        }

    }
}
