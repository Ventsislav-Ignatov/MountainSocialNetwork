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
        public async Task CreateCategoryCorrectly()
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

            await service.CreateCategory("testCategory");

            var expectedCount = 1;

            Assert.Equal(expectedCount, repositoryCategory.All().Count());
        }

        [Fact]
        public async Task DeleteCategoryCorrectly()
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


            var result = await service.CreateCategory("testCategory");

            await service.DeleteCategory(result);

            var expectedCount = 0;

            var articles = await repositoryArticle.All().Where(x => x.CategoryId == result).ToListAsync();

            Assert.Equal(expectedCount, repositoryCategory.All().Count());
            Assert.Equal(expectedCount, articles.Count());
        }

        [Fact]
        public async Task DeleteArticleCommentShoudlWorkCorrectly()
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

            await service.DeleteArticleComment(1);

            Assert.Equal(1, repositoryComment.AllWithDeleted().Count());
        }

        [Fact]
        public async Task DeleteNewsFeedCommentShouldWorkCorrectly()
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

            await service.DeleteNewsFeedComment(comment);

            Assert.Equal(1, repositoryNewsFeedComment.AllWithDeleted().Count());
        }

        [Fact]
        public async Task DeleteNewsFeedPostShouldWorkCorrectly()
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

            await service.DeleteNewsFeedPost(post);

            Assert.Equal(1, repositoryNewsFeedPost.AllWithDeleted().Count());
        }

        [Fact]
        public async Task UpdateNewsFeedPostShouldWorkCorrectly()
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

            await service.UpdateNewsFeedPost(postUpdate);

            var postAfterUpdate = await repositoryNewsFeedPost.All().FirstOrDefaultAsync(a => a.Id == postUpdate.Id);

            Assert.Equal(postUpdate.Content, postAfterUpdate.Content);
        }

        [Fact]
        public async Task GetNewsFeedPostShouldReturnCorrectly()
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

            var result = await service.GetNewsFeedPost(post.Id);

            Assert.IsType<NewsFeedPost>(result);
        }

        [Fact]
        public async Task DeleteArticleShouldReturnCorrectCount()
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

            await service.DeleteArticle(articleOne);

            Assert.Equal(1, repositoryArticle.All().Count());
        }

        [Fact]
        public async Task GetArticleByIdShouldReturnCorrectTypeAndArticle()
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

            var article = await service.GetArticle(articleOne.Id);

            Assert.IsType<Article>(article);
            Assert.Equal(articleOne, article);
        }

        [Fact]
        public async Task UpdateArticleShouldWorkCorrectly()
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

            await service.UpdateArticle(postUpdate);

            var postAfterUpdate = await repositoryArticle.All().FirstOrDefaultAsync(a => a.Id == postUpdate.Id);

            Assert.Equal(postUpdate.Title, postAfterUpdate.Title);
            Assert.Equal(postUpdate.Content, postAfterUpdate.Content);
        }

        [Fact]
        public async Task GetNewsFeedCommentByIdShouldReturnCorrectly()
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

            var comment = await service.GetNewsFeedComment(newsFeedComment.Id);

            Assert.IsType<NewsFeedComment>(comment);
            Assert.Equal(newsFeedComment, comment);
        }

        [Fact]
        public async Task UpdateCommentShouldUpdateCorrectly()
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

            var resultAfterUpdate = await service.UpdateComment(newsFeedCommentUpdate);


            Assert.IsType<NewsFeedComment>(resultAfterUpdate);
            Assert.Equal(newsFeedCommentUpdate.Content, resultAfterUpdate.Content);
        }
    }
}
