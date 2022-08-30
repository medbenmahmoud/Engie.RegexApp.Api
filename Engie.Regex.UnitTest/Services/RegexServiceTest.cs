using Engie.RegexApp.Api.Models;
using System;
using Xunit;
using Engie.RegexApp.UnitTest.Data;
using System.Linq;
using Engie.RegexApp.Api.Data;
using Engie.RegexApp.Api.Services;

namespace Engie.RegexApp.UnitTest
{
    public class RegexServiceTest
    {

        [Theory]
        [ClassData(typeof(ValidRegexValues))]
        public void AssertRegexResults(RegexRequest regexRequest, RegexMatchResult regexMatchResult)
        {
            var result = RegexService.Instance.CheckRegexExpression(regexRequest);
            Assert.NotNull(result);
            Assert.Equal(result.IsMatch, regexMatchResult.IsMatch);
            Assert.Equal(result.SubstitutionText, regexMatchResult.SubstitutionText);
            Assert.True(result.MatchDetails.Count == regexMatchResult.MatchDetails.Count);
            if (regexMatchResult.MatchDetails.Any())
            {
                for (int key = 0; key < regexMatchResult.MatchDetails.Count; ++key)
                {
                    var resultMatch = result.MatchDetails.ElementAt(key);
                    var validMatch = regexMatchResult.MatchDetails.ElementAt(key);
                    if(validMatch.FullMatch != null)
                    {
                        Assert.NotNull(resultMatch.FullMatch);
                        Assert.Equal(resultMatch.FullMatch.Index, validMatch.FullMatch.Index);
                        Assert.Equal(resultMatch.FullMatch.Length, validMatch.FullMatch.Length);
                        Assert.Equal(resultMatch.FullMatch.value, validMatch.FullMatch.value);
                    }
                    if(validMatch.Groups != null)
                    {
                        Assert.NotNull(resultMatch.Groups);
                        Assert.Equal(validMatch.Groups.Count, resultMatch.Groups.Count);
                        for (int grpKey = 0; grpKey < validMatch.Groups.Count; ++grpKey)
                        {
                            var resultGroup = resultMatch.Groups.ElementAt(grpKey);
                            var validGroup  = validMatch.Groups.ElementAt(grpKey);
                            Assert.NotNull(resultGroup);
                            Assert.Equal(validGroup.Index, resultGroup.Index);
                            Assert.Equal(validGroup.Length, resultGroup.Length);
                            Assert.Equal(validGroup.value, resultGroup.value);
                        }
                    }
                    
                }
            }
        }

        [Fact]
        public void AssertFlagsRegexCount()
        {            
            var result = RegexService.Instance.GetRegexFlagsIds();
            var flagsCount = Enum.GetValues(typeof(SpecificRegexOptions)).Length;
            Assert.Equal(result.Count, flagsCount);
        }
    }
}
