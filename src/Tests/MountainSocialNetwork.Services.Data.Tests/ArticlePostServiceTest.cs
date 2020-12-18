namespace MountainSocialNetwork.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Moq;
    using MountainSocialNetwork.Data;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Data.Repositories;
    using MountainSocialNetwork.Services.Mapping;
    using MountainSocialNetwork.Web.ViewModels.BlogPosts;
    using MountainSocialNetwork.Web.ViewModels.UsersPosts;
    using Xunit;

    public class ArticlePostServiceTest
    {
        [Fact]
        public async Task CreateArticleShouldWorkCorrectlyAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryArticlePicture = new EfRepository<ArticlePicture>(dbContext);
            var repositoryUserFavouriteArticle = new EfDeletableEntityRepository<UserFavouriteArticle>(dbContext);

            var service = new ArticlePostsService(repositoryArticle, repositoryArticlePicture, repositoryUserFavouriteArticle);

            await service.CreateAsync("test", "test", "1", 1);
            await service.CreateAsync("testTwo", "testTwo", "1", 2);

            var result = repositoryArticle.All().Count();

            Assert.Equal(2, result);
        }

        [Fact]
        public async Task CreateArticlePictureShouldWorkCorrectlyAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryArticlePicture = new EfRepository<ArticlePicture>(dbContext);
            var repositoryUserFavouriteArticle = new EfDeletableEntityRepository<UserFavouriteArticle>(dbContext);

            var service = new ArticlePostsService(repositoryArticle, repositoryArticlePicture, repositoryUserFavouriteArticle);

            await service.CreateArticlePicturesAsync(1, "1", "Test");
            await service.CreateArticlePicturesAsync(1, "1","TestTwo");
            await service.CreateArticlePicturesAsync(1, "1","TestThree");
            var result = repositoryArticlePicture.All().Count();

            Assert.Equal(3, result);
        }

        [Fact]

        public async Task AddFavouritePostShouldWorkCorrectlyAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);


            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryArticlePicture = new EfRepository<ArticlePicture>(dbContext);
            var repositoryUserFavouriteArticle = new EfDeletableEntityRepository<UserFavouriteArticle>(dbContext);

            var service = new ArticlePostsService(repositoryArticle, repositoryArticlePicture, repositoryUserFavouriteArticle);

            await service.AddFavouritePostAsync(1, "1");
            await service.AddFavouritePostAsync(2, "1");

            var result = repositoryUserFavouriteArticle.All().Count();

            Assert.Equal(2, result);
        }

        [Fact]
        public async Task MustReturnTrueWhenArticleIsAddedToFavouriteAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryArticlePicture = new EfRepository<ArticlePicture>(dbContext);
            var repositoryUserFavouriteArticle = new EfDeletableEntityRepository<UserFavouriteArticle>(dbContext);

            var service = new ArticlePostsService(repositoryArticle, repositoryArticlePicture, repositoryUserFavouriteArticle);

            await service.AddFavouritePostAsync(1, "1");

            var result = service.AlreadyAddedAsync(1, "1");

            Assert.True(result.Result);
        }

        [Fact]
        public async Task MustReturnFalseWhenArticleIsAddedToFavouriteAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryArticlePicture = new EfRepository<ArticlePicture>(dbContext);
            var repositoryUserFavouriteArticle = new EfDeletableEntityRepository<UserFavouriteArticle>(dbContext);

            var service = new ArticlePostsService(repositoryArticle, repositoryArticlePicture, repositoryUserFavouriteArticle);

            var result = await service.AlreadyAddedAsync(1, "1");

            Assert.False(result);
        }

        [Fact]
        public async Task UpdateShouldWorkCorrectlyAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryArticlePicture = new EfRepository<ArticlePicture>(dbContext);
            var repositoryUserFavouriteArticle = new EfDeletableEntityRepository<UserFavouriteArticle>(dbContext);

            var service = new ArticlePostsService(repositoryArticle, repositoryArticlePicture, repositoryUserFavouriteArticle);

            var articleId = await service.CreateAsync("test", "test", "1", 1);

            var article = new Article
            {
                Id = articleId,
                Title = "TestTwo",
                Content = "TestTwo",
                UserId = "1",
                CategoryId = 1,
            };

            var result = await service.UpdateAsync(article);

            Assert.Equal(article, result);
        }

        [Fact]
        public async Task ReturnTrueIfUserIsOwnerOfArticleAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryArticlePicture = new EfRepository<ArticlePicture>(dbContext);
            var repositoryUserFavouriteArticle = new EfDeletableEntityRepository<UserFavouriteArticle>(dbContext);

            var service = new ArticlePostsService(repositoryArticle, repositoryArticlePicture, repositoryUserFavouriteArticle);

            var articleId = await service.CreateAsync("test", "test", "1", 1);

            var isTrue = await service.ExistsAsync(articleId, "1");

            Assert.True(isTrue);
        }

        [Fact]
        public async Task ReturnFalseIfUserIsOwnerOfArticleAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryArticlePicture = new EfRepository<ArticlePicture>(dbContext);
            var repositoryUserFavouriteArticle = new EfDeletableEntityRepository<UserFavouriteArticle>(dbContext);

            var service = new ArticlePostsService(repositoryArticle, repositoryArticlePicture, repositoryUserFavouriteArticle);

            var articleId = await service.CreateAsync("test", "test", "1", 1);

            var isTrue = await service.ExistsAsync(articleId, "2");

            Assert.False(isTrue);
        }

        [Fact]
        public async Task GetAllFavouritePostAsyncShouldReturnCorrectCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryArticlePicture = new EfRepository<ArticlePicture>(dbContext);
            var repositoryUserFavouriteArticle = new EfDeletableEntityRepository<UserFavouriteArticle>(dbContext);

            var userFavouriteArticle = new UserFavouriteArticle
            {
                ArticleId = 1,
                UserId = "1",
            };

            await repositoryUserFavouriteArticle.AddAsync(userFavouriteArticle);
            await repositoryUserFavouriteArticle.SaveChangesAsync();

            AutoMapperConfig.RegisterMappings(typeof(GetAllFavouritePostTestViewModel).GetTypeInfo().Assembly);

            var service = new ArticlePostsService(repositoryArticle, repositoryArticlePicture, repositoryUserFavouriteArticle);

            var posts = await service.GetAllFavouritePostAsync<GetAllFavouritePostTestViewModel>(userFavouriteArticle.UserId);

            Assert.Equal(1, posts.Count());
        }

        [Fact]
        public async Task GetAllUserPostAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryArticlePicture = new EfRepository<ArticlePicture>(dbContext);
            var repositoryUserFavouriteArticle = new EfDeletableEntityRepository<UserFavouriteArticle>(dbContext);

            var article = new Article
            {
                Title = "Test",
                Content = "Test",
                CategoryId = 1,
                UserId = "1",
            };

            await repositoryArticle.AddAsync(article);
            await repositoryArticle.SaveChangesAsync();

            AutoMapperConfig.RegisterMappings(typeof(ArticleByUserViewModelTest).GetTypeInfo().Assembly);

            var service = new ArticlePostsService(repositoryArticle, repositoryArticlePicture, repositoryUserFavouriteArticle);

            var posts = await service.GetAllAsync<ArticleByUserViewModelTest>(article.UserId);

            Assert.Equal(1, posts.Count());
        }

        [Fact]
        public async Task GetArticleById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryArticlePicture = new EfRepository<ArticlePicture>(dbContext);
            var repositoryUserFavouriteArticle = new EfDeletableEntityRepository<UserFavouriteArticle>(dbContext);

            var article = new Article
            {
                Id = 1,
                Title = "Test",
                Content = "Test",
                CategoryId = 1,
                UserId = "1",
            };

            await repositoryArticle.AddAsync(article);
            await repositoryArticle.SaveChangesAsync();

            AutoMapperConfig.RegisterMappings(typeof(ArticleByIdTestViewModel).GetTypeInfo().Assembly);

            var service = new ArticlePostsService(repositoryArticle, repositoryArticlePicture, repositoryUserFavouriteArticle);

            var articleResult = await service.GetByIdAsync<ArticleByIdTestViewModel>(article.Id);

            Assert.Equal(article.Id, articleResult.Id);
        }
    }

    public class ArticleByIdTestViewModel : IMapFrom<Article>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string CategoryId { get; set; }
    }

    public class ArticleByUserViewModelTest : IMapFrom<Article>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string CategoryId { get; set; }
    }

    public class GetAllFavouritePostTestViewModel : IMapFrom<UserFavouriteArticle>
    {
        public string UserId { get; set; }

        public int ArticleId { get; set; }
    }
}
