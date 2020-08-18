using Conginzant.Domain.Challenges;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cognizant.Application.Services
{
    public interface IChallangesService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task UpdateUserAsync(string username, string task);
        Task<IEnumerable<Challenge>> GetChallengesAsync();
        Task<bool> PostSolutionAsync(string solution, string challengeId);
    }
}
