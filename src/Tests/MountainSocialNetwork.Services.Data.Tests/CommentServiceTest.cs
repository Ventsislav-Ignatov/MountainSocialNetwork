namespace MountainSocialNetwork.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MountainSocialNetwork.Data;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Data.Repositories;
    using Xunit;

    public class CommentServiceTest
    {
        [Fact]
        public async Task CreateCommentShouldWorkCorrectlyAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Comment>(dbContext);

            var service = new CommentsService(repository);

            var comment = new Comment
            {
                ArticleId = 1,
                UserId = "1",
                Content = "test",
                ParentId = null,
            };

            await service.CreateAsync(comment.ArticleId, comment.UserId, comment.Content, comment.ParentId);
            dbContext.SaveChanges();

            var count = repository.All().CountAsync();

            Assert.Equal(1, count.Result);
        }

        [Fact]
        public async Task IsInSamePostReturnTrueAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Comment>(dbContext);

            var service = new CommentsService(repository);

            var comment = new Comment
            {
                ArticleId = 1,
                UserId = "1",
                Content = "test",
                ParentId = null,
            };
            var commentWithParent = new Comment
            {
                ArticleId = 1,
                UserId = "1",
                Content = "test",
                ParentId = 1,
            };

            await service.CreateAsync(comment.ArticleId, comment.UserId, comment.Content, comment.ParentId);
            var result = service.IsInPostId((int)commentWithParent.ParentId, commentWithParent.ArticleId);

            dbContext.SaveChanges();

            Assert.True(result);
        }
    }
}
