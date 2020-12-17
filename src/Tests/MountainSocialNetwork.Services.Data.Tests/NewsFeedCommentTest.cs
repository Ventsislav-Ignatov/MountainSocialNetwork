namespace MountainSocialNetwork.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MountainSocialNetwork.Data;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Data.Repositories;
    using MountainSocialNetwork.Services.Data.NewsFeed;
    using Xunit;

    public class NewsFeedCommentTest
    {
        [Fact]
        public async Task CreateArticleShouldWorkCorrectlyAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            ApplicationDbContext dbContext = new ApplicationDbContext(options);

            var repositoryComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);

            var service = new NewsFeedCommentService(repositoryComment);

            await service.CreateAsync(1, "1", "TestComment");
            await service.CreateAsync(1, "1", "TestCommentTwo");

            var result = repositoryComment.All().Count();

            Assert.Equal(2, result);

        }
    }
}
