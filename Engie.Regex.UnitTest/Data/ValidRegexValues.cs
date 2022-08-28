using Engie.RegexApp.Api.Data;
using Engie.RegexApp.Api.Models;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Engie.RegexApp.UnitTest.Data
{
    public class ValidRegexValues : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                new RegexRequest()
                {
                    Pattern = "(?ix) d \\w+ \\s",
                    Text = "Dogs are decidedly good pets.",
                    FlagIds = new List<int>(){ (int)SpecificRegexOptions.Global }
                },
                new RegexMatchResult()
                {
                    IsMatch = true,
                    MatchDetails  = new List<MatchDetail>(){
                        new MatchDetail(){FullMatch = new RegexMatch(){Index = 0, Length = 5, value = "Dogs "}},
                        new MatchDetail(){FullMatch = new RegexMatch(){Index = 9, Length = 10, value = "decidedly "}},
                    }

                }
            };

            yield return new object[] {
                new RegexRequest()
                {
                    Pattern = "(\\+33|0) ?(\\d) ?(\\d\\d) ?(\\d\\d) ?(\\d\\d) ?(\\d\\d)",
                    Text = "My phone is 06 12 23 45 56.\n" +
                       "Can you confirm that your phone is +33 6 11 22 33 44 ?\n" +
                       "I think that 0123456789 is the best phone number",
                    FlagIds = new List<int>() { (int)RegexOptions.Multiline, (int)SpecificRegexOptions.Global },
                    Substitution = "0$2-$3-$4-$5-$5-$6"
                },
                new RegexMatchResult()
                {
                    IsMatch = true,
                    SubstitutionText = "My phone is 06-12-23-45-45-56.\n"+
                                       "Can you confirm that your phone is 06-11-22-33-33-44 ?\n"+
                                       "I think that 01-23-45-67-67-89 is the best phone number",
                    MatchDetails  = new List<MatchDetail>(){
                        new MatchDetail()
                        {
                            FullMatch = new RegexMatch(){Index = 12, Length = 14, value = "06 12 23 45 56"},
                            Groups = new List<RegexMatch>()
                            {
                                new RegexMatch(){Index = 12, Length = 1, value = "0"},
                                new RegexMatch(){Index = 13, Length = 1, value = "6"},
                                new RegexMatch(){Index = 15, Length = 2, value = "12"},
                                new RegexMatch(){Index = 18, Length = 2, value = "23"},
                                new RegexMatch(){Index = 21, Length = 2, value = "45"},
                                new RegexMatch(){Index = 24, Length = 2, value = "56"}
                            }
                        },
                        new MatchDetail()
                        {
                            FullMatch = new RegexMatch(){Index = 63, Length = 17, value = "+33 6 11 22 33 44"},
                            Groups = new List<RegexMatch>()
                            {
                                new RegexMatch(){Index = 63, Length = 3, value = "+33"},
                                new RegexMatch(){Index = 67, Length = 1, value = "6"},
                                new RegexMatch(){Index = 69, Length = 2, value = "11"},
                                new RegexMatch(){Index = 72, Length = 2, value = "22"},
                                new RegexMatch(){Index = 75, Length = 2, value = "33"},
                                new RegexMatch(){Index = 78, Length = 2, value = "44"}
                            }
                        },
                        new MatchDetail()
                        {
                            FullMatch = new RegexMatch(){Index = 96, Length = 10, value = "0123456789"},
                            Groups = new List<RegexMatch>()
                            {
                                new RegexMatch(){Index = 96, Length = 1, value = "0"},
                                new RegexMatch(){Index = 97, Length = 1, value = "1"},
                                new RegexMatch(){Index = 98, Length = 2, value = "23"},
                                new RegexMatch(){Index = 100, Length = 2, value = "45"},
                                new RegexMatch(){Index = 102, Length = 2, value = "67"},
                                new RegexMatch(){Index = 104, Length = 2, value = "89"}
                            }
                        }
                    }

                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }
}
