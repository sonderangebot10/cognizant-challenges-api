using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cognizant.ChallangesApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CognizantChallengesController : ControllerBase
    {
        //https://projecteuler.net/
        private static readonly List<Challenge> Challenges = new List<Challenge>()
        {
            new Challenge { Id = Guid.NewGuid().ToString("N"), Name = "Multiples of 3 and 5", Description = "Multiples of 3 and 5" },
            new Challenge { Id = Guid.NewGuid().ToString("N"), Name = "Fibonacci", Description = "Fibonacci" },
            new Challenge { Id = Guid.NewGuid().ToString("N"), Name = "Largest prime factor", Description = "Largest prime factor" }
        };

        private static readonly List<Player> Players = new List<Player>()
        {
            new Player() { Name = "Justas", SuccessSolutions = 2, Tasks = new List<string>(){ "1", "2" } },
            new Player() { Name = "Jonas", SuccessSolutions = 3, Tasks = new List<string>(){ "1", "2", "3" } },
            new Player() { Name = "Domante", SuccessSolutions = 5, Tasks = new List<string>(){ "1", "2", "3", "4", "5" } },
            new Player() { Name = "Noras", SuccessSolutions = 2, Tasks = new List<string>(){ "1", "2" } }
        };

        public class Challenge
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Input { get; set; }
            public string Output { get; set; }
        }

        public class Player
        {
            public string Name { get; set; }
            public int SuccessSolutions { get; set; }
            public IEnumerable<string> Tasks { get; set; }
        }

        private readonly ILogger<CognizantChallengesController> _logger;

        public CognizantChallengesController(ILogger<CognizantChallengesController> logger)
        {
            _logger = logger;
        }

        [HttpGet("challenges")]
        public IEnumerable<Challenge> GetChallenges()
        {
            return Challenges;
        }

        [HttpGet("players")]
        public IEnumerable<Player> GetPlayers()
        {
            return Players;
        }

        [HttpPost("{challengeId}")]
        public bool PostChallenge()
        {
            return false;
        }
    }
}
