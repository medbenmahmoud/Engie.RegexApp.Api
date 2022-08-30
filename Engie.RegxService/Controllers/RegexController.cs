using Engie.RegexApp.Api;
using Engie.RegexApp.Api.Data;
using Engie.RegexApp.Api.Models;
using Engie.RegexApp.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;

namespace Engie.RegxApp.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RegexController : ControllerBase
    {
        private readonly IStringLocalizer<Resource> _localizer;

        public RegexController(IStringLocalizer<Resource> localizer)
        {
            _localizer = localizer;
        }

        [HttpGet("flags")]
        public ActionResult<IList<RegexFlagDetails>> GetFlags()
        {
            var flagIds = RegexService.Instance.GetRegexFlagsIds();
            var result = new List<RegexFlagDetails>();
            foreach (int flagId in flagIds)
            {
                result.Add(new RegexFlagDetails()
                {
                    Id = flagId,
                    Code = _localizer["REGEX_FLAG_CODE_" + flagId].ResourceNotFound ? string.Empty : _localizer["REGEX_FLAG_CODE_" + flagId],
                    Name = _localizer["REGEX_FLAG_NAME_" + flagId].ResourceNotFound ? string.Empty : _localizer["REGEX_FLAG_NAME_" + flagId],
                    Description = _localizer["REGEX_FLAG_DESC_" + flagId].ResourceNotFound ? string.Empty : _localizer["REGEX_FLAG_DESC_" + flagId]
                });
            }
            return result;
        }

        [HttpPost()]
        public ActionResult<RegexMatchResult> FindMatch(RegexRequest regexRequest)
        {
            if (regexRequest.Pattern == null)
                return BadRequest("Pattern cannot be null");

            if (regexRequest.Text == null)
                return BadRequest("Text cannot be null");

            if (regexRequest.FlagIds != null)
            {
                foreach(var id in regexRequest.FlagIds)
                {
                    if(!RegexFlag.IsValidRegexOptionsId(id))
                        return NotFound($"Flag Id {id} not found");
                }
                
            }
            var result = RegexService.Instance.CheckRegexExpression(regexRequest);
            result.Message = result.IsMatch ? _localizer["REGEX_MATCH_MESSAGE"] : _localizer["REGEX_NOT_MATCH_MESSAGE"];

            return result;
        }

        
    }
}
