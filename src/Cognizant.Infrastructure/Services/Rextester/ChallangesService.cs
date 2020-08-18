using Cognizant.ChallengesApi.Controllers.Challanges.jsons;
using Cognizant.Infrastructure.Data.PgSql.Challenges.Repositories;
using Conginzant.Domain.Challenges;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cognizant.Infrastructure.Services.Rextester
{
    class ChallangesService : IChallangesService
    {
        private readonly ILogger<ChallangesService> _logger;
        private readonly IChallengesRepository _repo;
        private readonly string _uri;
        private readonly HttpClient _client;

        public ChallangesService(Context context, ILogger<ChallangesService> logger, IChallengesRepository repo)
        {   
            _uri = context?.Uri ?? throw new ArgumentNullException(nameof(context.Uri));
            _logger = logger;
            _client = new HttpClient();
            _repo = repo;
        }

        public async Task<IEnumerable<Challenge>> GetChallengesAsync()
        {
            return await _repo.GetChallengesAsync();
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _repo.GetUsersAsync();
        }

        public async Task<bool> PostSolution(string solution, string challengeId)
        {
            var challanges = await _repo.GetChallengesAsync();
            var challenge = challanges?.Where(x => x.Id.Equals(challengeId)).FirstOrDefault();

            var request = JsonConvert.SerializeObject(new RequestModel() { Program = solution, Input = challenge.InputParam });

            var response = await _client.PostAsync(_uri, new StringContent(request, Encoding.UTF8, "application/json"));
            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ResponseModel>(responseString)?.Result; 

            if(result.Equals(challenge.OutputParam))
            {
                return true;
            }

            return false;
        }

        public async Task UpdateUserAsync(string username, string task)
        {
            await _repo.UpdateUserAsync(username, task);
        }
    }
}
