namespace MountainSocialNetwork.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MountainSocialNetwork.Data;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Data.Repositories;
    using MountainSocialNetwork.Services.Data.Administrator;
    using Xunit;

    public class AdministratorServiceTest
    {
        [Fact]
        public async Task CreateCategoryCorrectlyAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var repositoryNewsFeedComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var repositoryCategory = new EfDeletableEntityRepository<Category>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<Comment>(dbContext);

            var service = new AdministratorService(repositoryNewsFeedPost, repositoryArticle, repositoryNewsFeedComment, repositoryCategory, repositoryComment);

            await service.CreateCategoryAsync("testCategory");

            var expectedCount = 1;

            Assert.Equal(expectedCount, repositoryCategory.All().Count());
        }

        [Fact]
        public async Task DeleteCategoryCorrectlyAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var repositoryNewsFeedComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var repositoryCategory = new EfDeletableEntityRepository<Category>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<Comment>(dbContext);

            var service = new AdministratorService(repositoryNewsFeedPost, repositoryArticle, repositoryNewsFeedComment, repositoryCategory, repositoryComment);


            var result = await service.CreateCategoryAsync("testCategory");

            await service.DeleteCategoryAsync(result);

            var expectedCount = 0;

            var articles = await repositoryArticle.All().Where(x => x.CategoryId == result).ToListAsync();

            Assert.Equal(expectedCount, repositoryCategory.All().Count());
            Assert.Equal(expectedCount, articles.Count());
        }

        [Fact]
        public async Task DeleteArticleCommentShoudlWorkCorrectlyAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var repositoryNewsFeedComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var repositoryCategory = new EfDeletableEntityRepository<Category>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<Comment>(dbContext);

            var service = new AdministratorService(repositoryNewsFeedPost, repositoryArticle, repositoryNewsFeedComment, repositoryCategory, repositoryComment);

            var comment = new Comment
            {
                Id = 1,
                ArticleId = 1,
                Content = "test",
                UserId = "1",
            };

            await repositoryComment.AddAsync(comment);

            await repositoryComment.SaveChangesAsync();

            await service.DeleteArticleCommentAsync(1);

            Assert.Equal(1, repositoryComment.AllWithDeleted().Count());
        }

        [Fact]
        public async Task DeleteNewsFeedCommentShouldWorkCorrectlyAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var repositoryNewsFeedComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var repositoryCategory = new EfDeletableEntityRepository<Category>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<Comment>(dbContext);

            var service = new AdministratorService(repositoryNewsFeedPost, repositoryArticle, repositoryNewsFeedComment, repositoryCategory, repositoryComment);

            var comment = new NewsFeedComment
            {
                Id = 1,
                Content = "test",
                UserId = "1",
            };

            await repositoryNewsFeedComment.AddAsync(comment);

            await repositoryNewsFeedComment.SaveChangesAsync();

            await service.DeleteNewsFeedCommentAsync(comment);

            Assert.Equal(1, repositoryNewsFeedComment.AllWithDeleted().Count());
        }

        [Fact]
        public async Task DeleteNewsFeedPostShouldWorkCorrectlyAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var repositoryNewsFeedComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var repositoryCategory = new EfDeletableEntityRepository<Category>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<Comment>(dbContext);

            var service = new AdministratorService(repositoryNewsFeedPost, repositoryArticle, repositoryNewsFeedComment, repositoryCategory, repositoryComment);

            var post = new NewsFeedPost
            {
                Id = 1,
                Content = "test",
                UserId = "1",
            };

            await repositoryNewsFeedPost.AddAsync(post);

            await repositoryNewsFeedPost.SaveChangesAsync();

            await service.DeleteNewsFeedPostAsync(post);

            Assert.Equal(1, repositoryNewsFeedPost.AllWithDeleted().Count());
        }

        [Fact]
        public async Task UpdateNewsFeedPostShouldWorkCorrectlyAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var repositoryNewsFeedComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var repositoryCategory = new EfDeletableEntityRepository<Category>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<Comment>(dbContext);

            var service = new AdministratorService(repositoryNewsFeedPost, repositoryArticle, repositoryNewsFeedComment, repositoryCategory, repositoryComment);

            var post = new NewsFeedPost
            {
                Id = 1,
                Content = "test",
                UserId = "1",
            };

            await repositoryNewsFeedPost.AddAsync(post);

            await repositoryNewsFeedPost.SaveChangesAsync();

            var postUpdate = new NewsFeedPost
            {
                Id = 1,
                Content = "testtttt",
                UserId = "1",
            };

            await service.UpdateNewsFeedPostAsync(postUpdate);

            var postAfterUpdate = await repositoryNewsFeedPost.All().FirstOrDefaultAsync(a => a.Id == postUpdate.Id);

            Assert.Equal(postUpdate.Content, postAfterUpdate.Content);
        }

        [Fact]
        public async Task GetNewsFeedPostShouldReturnCorrectlyAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var repositoryNewsFeedComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var repositoryCategory = new EfDeletableEntityRepository<Category>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<Comment>(dbContext);

            var service = new AdministratorService(repositoryNewsFeedPost, repositoryArticle, repositoryNewsFeedComment, repositoryCategory, repositoryComment);

            var post = new NewsFeedPost
            {
                Id = 1,
                Content = "test",
                UserId = "1",
            };


            await repositoryNewsFeedPost.AddAsync(post);

            await repositoryNewsFeedPost.SaveChangesAsync();

            var result = await service.GetNewsFeedPostAsync(post.Id);

            Assert.IsType<NewsFeedPost>(result);
        }

        [Fact]
        public async Task DeleteArticleShouldReturnCorrectCountAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var repositoryNewsFeedComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var repositoryCategory = new EfDeletableEntityRepository<Category>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<Comment>(dbContext);

            var service = new AdministratorService(repositoryNewsFeedPost, repositoryArticle, repositoryNewsFeedComment, repositoryCategory, repositoryComment);

            var articleOne = new Article
            {
                Id = 1,
                Title = "test",
                Content = "test",
                UserId = "1",
            };

            var articleTwo = new Article
            {
                Id = 2,
                Title = "testTwo",
                Content = "testTwo",
                UserId = "1",
            };

            await repositoryArticle.AddAsync(articleOne);
            await repositoryArticle.AddAsync(articleTwo);

            await repositoryArticle.SaveChangesAsync();

            await service.DeleteArticleAsync(articleOne);

            Assert.Equal(1, repositoryArticle.All().Count());
        }

        [Fact]
        public async Task GetArticleByIdShouldReturnCorrectTypeAndArticleAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var repositoryNewsFeedComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var repositoryCategory = new EfDeletableEntityRepository<Category>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<Comment>(dbContext);

            var service = new AdministratorService(repositoryNewsFeedPost, repositoryArticle, repositoryNewsFeedComment, repositoryCategory, repositoryComment);

            var articleOne = new Article
            {
                Id = 1,
                Title = "test",
                Content = "test",
                UserId = "1",
            };
            await repositoryArticle.AddAsync(articleOne);

            await repositoryArticle.SaveChangesAsync();

            var article = await service.GetArticleAsync(articleOne.Id);

            Assert.IsType<Article>(article);
            Assert.Equal(articleOne, article);
        }

        [Fact]
        public async Task UpdateArticleShouldWorkCorrectlyAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var repositoryNewsFeedComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var repositoryCategory = new EfDeletableEntityRepository<Category>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<Comment>(dbContext);

            var service = new AdministratorService(repositoryNewsFeedPost, repositoryArticle, repositoryNewsFeedComment, repositoryCategory, repositoryComment);

            var post = new Article
            {
                Id = 1,
                Title = "test",
                Content = "test",
                UserId = "1",
            };

            await repositoryArticle.AddAsync(post);

            await repositoryArticle.SaveChangesAsync();

            var postUpdate = new Article
            {
                Id = 1,
                Content = "testtttt",
                Title = "testtttt",
                UserId = "1",
            };

            await service.UpdateArticleAsync(postUpdate);

            var postAfterUpdate = await repositoryArticle.All().FirstOrDefaultAsync(a => a.Id == postUpdate.Id);

            Assert.Equal(postUpdate.Title, postAfterUpdate.Title);
            Assert.Equal(postUpdate.Content, postAfterUpdate.Content);
        }

        [Fact]
        public async Task GetNewsFeedCommentByIdShouldReturnCorrectlyAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var repositoryNewsFeedComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var repositoryCategory = new EfDeletableEntityRepository<Category>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<Comment>(dbContext);

            var service = new AdministratorService(repositoryNewsFeedPost, repositoryArticle, repositoryNewsFeedComment, repositoryCategory, repositoryComment);

            var newsFeedComment = new NewsFeedComment
            {
                Id = 1,
                Content = "test",
                UserId = "1",
            };
            await repositoryNewsFeedComment.AddAsync(newsFeedComment);

            await repositoryNewsFeedComment.SaveChangesAsync();

            var comment = await service.GetNewsFeedCommentAsync(newsFeedComment.Id);

            Assert.IsType<NewsFeedComment>(comment);
            Assert.Equal(newsFeedComment, comment);
        }

        [Fact]
        public async Task UpdateCommentShouldUpdateCorrectlyAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var repositoryNewsFeedComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var repositoryCategory = new EfDeletableEntityRepository<Category>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<Comment>(dbContext);

            var service = new AdministratorService(repositoryNewsFeedPost, repositoryArticle, repositoryNewsFeedComment, repositoryCategory, repositoryComment);

            var newsFeedComment = new NewsFeedComment
            {
                Id = 1,
                Content = "test",
                UserId = "1",
            };
            await repositoryNewsFeedComment.AddAsync(newsFeedComment);

            await repositoryNewsFeedComment.SaveChangesAsync();

            var newsFeedCommentUpdate = new NewsFeedComment
            {
                Id = 1,
                Content = "testttt",
                UserId = "1",
            };

            var resultAfterUpdate = await service.UpdateCommentAsync(newsFeedCommentUpdate);

            Assert.IsType<NewsFeedComment>(resultAfterUpdate);
            Assert.Equal(newsFeedCommentUpdate.Content, resultAfterUpdate.Content);
        }
    }
}
