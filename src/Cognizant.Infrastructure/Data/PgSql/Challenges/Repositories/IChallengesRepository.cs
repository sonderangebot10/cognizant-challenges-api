using Conginzant.Domain.Challenges;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cognizant.Infrastructure.Data.PgSql.Challenges.Repositories
{
    public interface IChallengesRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task UpdateUserAsync(string username, string task);
        Task<IEnumerable<Challenge>> GetChallengesAsync();

    }
}
