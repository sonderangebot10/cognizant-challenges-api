using Conginzant.Domain.Challenges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cognizant.Infrastructure.Data.PgSql.Challenges.Repositories
{
    public class ChallengesRepository : IChallengesRepository
    {
        private readonly Context _context;

        public ChallengesRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Challenge>> GetChallengesAsync()
        {
            var EChallenges = _context.Challenges;
            var result = new List<Challenge>();
            foreach(var EChallenge in EChallenges)
            {
                var challenge = new Challenge(EChallenge.Id.ToString(), EChallenge.TaskName, EChallenge.Description, EChallenge.InputParam, EChallenge.OutputParam);
                result.Add(challenge);
            }
            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var EUsers = _context.Users;
            var result = new List<User>();
            foreach (var EUser in EUsers)
            {
                var user = new User(EUser.Name, EUser.SuccessSolutions, EUser.Tasks);
                result.Add(user);
            }
            return await Task.FromResult(result);
        }

        public async Task UpdateUserAsync(string username, string task)
        {
            var curUser = _context?.Users?.Where(x => x.Name.Equals(username)).FirstOrDefault();
            var challange = _context.Challenges?.Where(x => x.Id.Equals(Int32.Parse(task))).FirstOrDefault();

            if(challange == null)
            {
                return;
            }

            if (curUser == null)
            {
                var EUser = new Entities.User() { Name = username, SuccessSolutions = 1, Tasks = challange.TaskName };
                _context.Users.Add(EUser);
                await _context.SaveChangesAsync();
                return;
            }

            if(!curUser.Tasks.Split(",").Contains(challange.TaskName))
            {
                curUser.Tasks = curUser.Tasks + "," + challange.TaskName;
                curUser.SuccessSolutions = curUser.Tasks.Split(",").Length;
                await _context.SaveChangesAsync();
            }
        }
    }
}
