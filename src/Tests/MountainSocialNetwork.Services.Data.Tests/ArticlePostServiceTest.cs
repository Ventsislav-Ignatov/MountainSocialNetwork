namespace MountainSocialNetwork.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Moq;
    using MountainSocialNetwork.Data;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Data.Repositories;
    using MountainSocialNetwork.Web.ViewModels.BlogPosts;
    using MountainSocialNetwork.Web.ViewModels.UsersPosts;
    using Xunit;

    public class ArticlePostServiceTest
    {
        [Fact]
        public void CreateArticleShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryArticlePicture = new EfRepository<ArticlePicture>(dbContext);
            var repositoryUserFavouriteArticle = new EfDeletableEntityRepository<UserFavouriteArticle>(dbContext);

            var service = new ArticlePostsService(repositoryArticle, repositoryArticlePicture, repositoryUserFavouriteArticle);

            service.CreateAsync("test", "test", "1", 1);
            service.CreateAsync("testTwo", "testTwo", "1", 2);

            var result = repositoryArticle.All().Count();

            Assert.Equal(2, result);
        }

        [Fact]
        public void CreateArticlePictureShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);


            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryArticlePicture = new EfRepository<ArticlePicture>(dbContext);
            var repositoryUserFavouriteArticle = new EfDeletableEntityRepository<UserFavouriteArticle>(dbContext);

            var service = new ArticlePostsService(repositoryArticle, repositoryArticlePicture, repositoryUserFavouriteArticle);

            service.CreateArticlePicturesAsync(1, "1", "Test");
            service.CreateArticlePicturesAsync(1, "1","TestTwo");
            service.CreateArticlePicturesAsync(1, "1","TestThree");
            var result = repositoryArticlePicture.All().Count();

            Assert.Equal(3, result);
        }

        [Fact]

        public void AddFavouritePostShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);


            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryArticlePicture = new EfRepository<ArticlePicture>(dbContext);
            var repositoryUserFavouriteArticle = new EfDeletableEntityRepository<UserFavouriteArticle>(dbContext);

            var service = new ArticlePostsService(repositoryArticle, repositoryArticlePicture, repositoryUserFavouriteArticle);

            service.AddFavouritePost(1, "1");
            service.AddFavouritePost(2, "1");

            var result = repositoryUserFavouriteArticle.All().Count();

            Assert.Equal(2, result);
        }

        [Fact]
        public void MustReturnTrueWhenArticleIsAddedToFavourite()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryArticlePicture = new EfRepository<ArticlePicture>(dbContext);
            var repositoryUserFavouriteArticle = new EfDeletableEntityRepository<UserFavouriteArticle>(dbContext);

            var service = new ArticlePostsService(repositoryArticle, repositoryArticlePicture, repositoryUserFavouriteArticle);

            service.AddFavouritePost(1, "1");

            var result = service.AlreadyAdded(1, "1");

            Assert.True(result.Result);
        }

        [Fact]
        public void MustReturnFalseWhenArticleIsAddedToFavourite()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryArticlePicture = new EfRepository<ArticlePicture>(dbContext);
            var repositoryUserFavouriteArticle = new EfDeletableEntityRepository<UserFavouriteArticle>(dbContext);

            var service = new ArticlePostsService(repositoryArticle, repositoryArticlePicture, repositoryUserFavouriteArticle);

            var result = service.AlreadyAdded(1, "1");

            Assert.False(result.Result);
        }
        [Fact]
        public async Task UpdateShouldWorkCorrectly()
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

            var result = await service.Update(article);

            Assert.Equal(article, result);
        }

        [Fact]
        public async Task ReturnTrueIfUserIsOwnerOfArticle()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryArticlePicture = new EfRepository<ArticlePicture>(dbContext);
            var repositoryUserFavouriteArticle = new EfDeletableEntityRepository<UserFavouriteArticle>(dbContext);

            var service = new ArticlePostsService(repositoryArticle, repositoryArticlePicture, repositoryUserFavouriteArticle);

            var articleId = await service.CreateAsync("test", "test", "1", 1);

            var isTrue = await service.Exists(articleId, "1");

            Assert.True(isTrue);
        }

        [Fact]
        public async Task ReturnFalseIfUserIsOwnerOfArticle()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryArticlePicture = new EfRepository<ArticlePicture>(dbContext);
            var repositoryUserFavouriteArticle = new EfDeletableEntityRepository<UserFavouriteArticle>(dbContext);

            var service = new ArticlePostsService(repositoryArticle, repositoryArticlePicture, repositoryUserFavouriteArticle);

            var articleId = await service.CreateAsync("test", "test", "1", 1);

            var isTrue = await service.Exists(articleId, "2");

            Assert.False(isTrue);
        }
    }
}
