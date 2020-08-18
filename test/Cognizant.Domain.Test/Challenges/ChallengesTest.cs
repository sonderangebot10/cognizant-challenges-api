using Conginzant.Domain.Challenges;

using Xunit;

namespace Cognizant.Domain.Test.Challanges
{
    public class ChallengesTest
    {
        [Trait("Category", "UnitTest")]
        [Fact]
        public void Challenge_ctor()
        {
            var o = new Challenge("string", "string", "string", "string", "string");

            Assert.NotNull(o);
        }

        [Trait("Category", "UnitTest")]
        [Fact]
        public void User_ctor()
        {
            var o = new User("name", 1, "tasks");

            Assert.NotNull(o);
        }
    }
}
