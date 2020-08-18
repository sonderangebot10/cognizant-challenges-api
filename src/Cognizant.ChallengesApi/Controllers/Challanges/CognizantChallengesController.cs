using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cognizant.Infrastructure.Services.Rextester;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cognizant.ChallangesApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CognizantChallengesController : ControllerBase
    {
        private readonly IChallangesService _rexService;
        private readonly ILogger<CognizantChallengesController> _logger;

        public CognizantChallengesController(ILogger<CognizantChallengesController> logger, IChallangesService rexService)
        {
            _logger = logger;
            _rexService = rexService;
        }

        [HttpGet("challenges")]
        public async Task<IActionResult> GetChallenges()
        {
            var challanges = await _rexService.GetChallengesAsync();

            if (challanges.Count() == 0)
            {
                _logger.LogInformation("No challanges found!");
                return NoContent();
            }

            return Ok(challanges);
        }

        [HttpGet("players")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _rexService.GetUsersAsync();

            if(users.Count() == 0)
            {
                _logger.LogInformation("No users found!");
                return NoContent();
            }

            return Ok(users);
        }

        [HttpPost("{challengeId}")]
        public async Task<IActionResult> PostChallenge([FromBody] string solution, [FromHeader] string name, string challengeId)
        {
            var isCorrect = await _rexService.PostSolution(solution, challengeId);

            if(isCorrect)
            {
                await _rexService.UpdateUserAsync(name, challengeId);
            }

            return Ok(isCorrect);
        }
    }
}
