using Engie.RegexApp.Api.Data;
using Engie.RegexApp.Api.Interfaces;
using Engie.RegexApp.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Engie.RegxApp.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RegexController : ControllerBase
    {
        private readonly IRegexService _regexService;
        public RegexController(IRegexService regexService)
        {
            _regexService = regexService;
        }


        [HttpGet("flags")]
        public ActionResult<IList<RegexFlagDetails>> GetFlags()
        {
            var result = _regexService.GetRegexFlags();
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
            var result = _regexService.CheckRegexExpression(regexRequest);

            return result;
        }

        
    }
}
