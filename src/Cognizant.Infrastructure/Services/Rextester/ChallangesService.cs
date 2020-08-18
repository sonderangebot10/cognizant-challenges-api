using Cognizant.Application.Repositories;
using Cognizant.Application.Services;
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
    public class ChallangesService : IChallangesService
    {
        private readonly ILogger<ChallangesService> _logger;
        private readonly IChallengesRepository _repo;
        private readonly string _uri;
        private readonly HttpClient _client;

        public ChallangesService(Context context, ILogger<ChallangesService> logger, IChallengesRepository repo, HttpClient client = null)
        {   
            _uri = context?.Uri ?? throw new ArgumentNullException(nameof(context.Uri));
            _logger = logger;
            _client = client ?? new HttpClient();
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

        public async Task<bool> PostSolutionAsync(string solution, string challengeId)
        {
            var challanges = await _repo.GetChallengesAsync();
            var challenge = challanges?.Where(x => x.Id.Equals(challengeId)).FirstOrDefault();

            var requestContent = JsonConvert.SerializeObject(new RequestModel() { Program = solution, Input = challenge.InputParam });

            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_uri),
                Content = new StringContent(requestContent, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage response = await _client.SendAsync(request);

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
