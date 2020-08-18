using Conginzant.Domain.Challenges;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cognizant.Application.Repositories
{
    public interface IChallengesRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task UpdateUserAsync(string username, string task);
        Task<IEnumerable<Challenge>> GetChallengesAsync();

    }
}
