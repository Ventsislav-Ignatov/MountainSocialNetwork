namespace MountainSocialNetwork.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using MountainSocialNetwork.Data;
    using MountainSocialNetwork.Data.Common.Repositories;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Data.Repositories;
    using MountainSocialNetwork.Services.Data.Votes;
    using Xunit;

    public class VoteServiceTest
    {
        [Fact]
        public void RetunsCorrectUpVotesCount()
        {
            var voteDate = new List<Vote>
            {
                new Vote
                {
                    NewsFeedPostId = 1,
                    UserId = "1",
                    IsUpVote = true,
                },
                new Vote
                {
                    NewsFeedPostId = 1,
                    UserId = "2",
                    IsUpVote = true,
                },
            };

            var mock = new Mock<IRepository<Vote>>();

            mock.Setup(x => x.All()).Returns(voteDate.AsQueryable());

            var service = new VotesService(mock.Object);

            var result = service.GetUpVotes(1);

            Assert.Equal(2, result);
        }

        [Fact]
        public void RetunsCorrectDownVotesCount()
        {
            var voteDate = new List<Vote>
            {
                new Vote
                {
                    NewsFeedPostId = 1,
                    UserId = "1",
                    IsUpVote = false,
                },
                new Vote
                {
                    NewsFeedPostId = 1,
                    UserId = "2",
                    IsUpVote = false,
                },
            };

            var mock = new Mock<IRepository<Vote>>();

            mock.Setup(x => x.All()).Returns(voteDate.AsQueryable());

            var service = new VotesService(mock.Object);

            var result = service.GetDownVotes(1);

            Assert.Equal(2, result);
        }

        [Fact]
        public void CreateVoteShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);


            var repository = new EfRepository<Vote>(dbContext);

            var service = new VotesService(repository);

            var upVote = new Vote
            {
                NewsFeedPostId = 1,
                UserId = "1",
                IsUpVote = true,
            };
            var downVote = new Vote
            {
                NewsFeedPostId = 1,
                UserId = "2",
                IsUpVote = false,
            };

            service.VoteAsync(upVote.NewsFeedPostId, upVote.UserId, upVote.IsUpVote);

            service.VoteAsync(downVote.NewsFeedPostId, downVote.UserId, downVote.IsUpVote);

            dbContext.SaveChangesAsync();
            var resultUpVote = service.GetUpVotes(1);
            var resultDownVote = service.GetDownVotes(1);

            Assert.Equal(1, resultUpVote);
            Assert.Equal(1, resultDownVote);
        }

        [Fact]
        public void WhenUserVoteTwiceWithTrueForSamePostShouldCountOnlyOnce()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);


            var repository = new EfRepository<Vote>(dbContext);

            var service = new VotesService(repository);

            service.VoteAsync(1, "1", true);
            service.VoteAsync(1, "1", true);
            dbContext.SaveChangesAsync();
            var result = service.GetUpVotes(1);
            Assert.Equal(1, result);
        }

        [Fact]
        public void WhenUserVoteTwiceWithFalseForSamePostShouldCountOnlyOnce()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);


            var repository = new EfRepository<Vote>(dbContext);

            var service = new VotesService(repository);

            service.VoteAsync(1, "1", false);
            service.VoteAsync(1, "1", false);
            dbContext.SaveChangesAsync();
            var result = service.GetDownVotes(1);
            Assert.Equal(1, result);
        }
    }
}
