using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Engie.RegexApp.Api.Interfaces;
using Engie.RegexApp.Api.Models;
using Microsoft.Extensions.Localization;
using Engie.RegexApp.Api.Mappers;
using Engie.RegexApp.Api.Data;

namespace Engie.RegexApp.Api.Services
{
    public class RegexService : IRegexService
    {
        private readonly IStringLocalizer<Resource> _localizer;

        public RegexService(IStringLocalizer<Resource> localizer)
        {
            _localizer = localizer;
        }

        public RegexMatchResult CheckRegexExpression(RegexRequest regexRequest)
        {
            Regex rgx;
            if (regexRequest.FlagIds != null && regexRequest.FlagIds.Any())
            {
                var AggregatedOption = AggregateFlagsIds(regexRequest.FlagIds);
                rgx = new Regex(regexRequest.Pattern, AggregatedOption);
            }
            else
            {
                rgx = new Regex(regexRequest.Pattern);
            }

            var result = new RegexMatchResult();
            result.IsMatch = rgx.IsMatch(regexRequest.Text);
            result.Message = result.IsMatch ? _localizer["REGEX_MATCH_MESSAGE"] : _localizer["REGEX_NOT_MATCH_MESSAGE"];

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

        public List<RegexFlagDetails> GetRegexFlags()
        {
            var result = new List<RegexFlagDetails>();
            var flagIds = RegexFlag.GetRegexOptionsIds();
            foreach (int flagId in flagIds)
            {
                result.Add(new RegexFlagDetails() {
                    Id = flagId,
                    Code = _localizer["REGEX_FLAG_CODE_"+ flagId].ResourceNotFound ? string.Empty : _localizer["REGEX_FLAG_CODE_" + flagId],
                    Name= _localizer["REGEX_FLAG_NAME_"+ flagId].ResourceNotFound ? string.Empty : _localizer["REGEX_FLAG_NAME_" + flagId],
                    Description = _localizer["REGEX_FLAG_DESC_"+ flagId].ResourceNotFound ? string.Empty : _localizer["REGEX_FLAG_DESC_" + flagId]
                });
            }

            return result;
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
