using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Cognizant.Application.Repositories;
using Cognizant.Infrastructure.Services.Rextester;
using Conginzant.Domain.Challenges;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Xunit;

namespace Cognizant.Infrastructure.Test.Services
{
    public class ChallengesServiceTest
    {
        static IEnumerable<Challenge> mockChallenges = new List<Challenge>()
        {
            new Challenge("1", "Fibbonaci", "Fibonnaci description", "5", "5"),
            new Challenge("randId", "randTask", "randDesc", "randInt", "randInt")
        };

        static IEnumerable<User> mockUsers = new List<User>()
        {
            new User("Justas", 2, "Fibonacci,randTask"),
            new User("Domante", 1, "Fibonacci")
        };

        private static ChallangesService GetChallengesService()
        {

            var challengesRepo = new Mock<IChallengesRepository>();

            challengesRepo.Setup(e => e.GetChallengesAsync())
                .Returns(Task.FromResult(mockChallenges));
            challengesRepo.Setup(e => e.GetUsersAsync())
                .Returns(Task.FromResult(mockUsers));
            challengesRepo.Setup(e => e.UpdateUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent("{\"Warnings\":null,\"Errors\":null,\"Result\":\"5\",\"Stats\":\"Compilation time: 0,22 sec, absolute running time: 0,11 sec, cpu time: 0,12 sec, average memory usage: 15 Mb, average nr of threads: 3, absolute service time: 0,34 sec\",\"Files\":null,\"NotLoggedIn\":false}"),
               })
               .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            var challengesService = new ChallangesService(new Context("http://test.com/"), new Mock<ILogger<ChallangesService>>().Object, challengesRepo.Object, httpClient);

            return challengesService;
        }

        [Trait("Category", "UnitTest")]
        [Fact]
        public async void ChallangesService_GetChallangesAsync()
        {
            var client = GetChallengesService();

            var challenges = await client.GetChallengesAsync();

            Assert.NotNull(challenges);
            Assert.Equal(challenges, mockChallenges);
        }

        [Trait("Category", "UnitTest")]
        [Fact]
        public async void ChallangesService_GetUsersAsync()
        {
            var client = GetChallengesService();

            var users = await client.GetUsersAsync();

            Assert.NotNull(users);
            Assert.Equal(users, mockUsers);
        }

        [Trait("Category", "UnitTest")]
        [Fact]
        public async void ChallangesService_PostSolutionTrueAsync()
        {
            var client = GetChallengesService();

            var result = await client.PostSolutionAsync("5", "1");
            Assert.True(result);
        }

        [Trait("Category", "UnitTest")]
        [Fact]
        public async void ChallangesService_PostSolutionFalseAsync()
        {
            var client = GetChallengesService();

            var result = await client.PostSolutionAsync("7", "1");
            Assert.True(result);
        }
    }
}
