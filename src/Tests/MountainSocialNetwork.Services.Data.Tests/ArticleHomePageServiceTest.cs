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

    public class ArticleHomePageServiceTest
    {
        [Fact]
        public async Task GetArticlesPerPageShouldReturnCorrectCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);

            var articles = this.ArticleSeeder(6, DateTime.UtcNow);

            foreach (var article in articles)
            {
                await repositoryArticle.AddAsync(article);
            }

            await repositoryArticle.SaveChangesAsync();

            AutoMapperConfig.RegisterMappings(typeof(GetAllArticleByIdTestViewModel).GetTypeInfo().Assembly);

            var service = new ArticleHomePageService(repositoryArticle);

            var articleResult = service.GetAllArticlePostsAsync<GetAllArticleByIdTestViewModel>(1);

            Assert.Equal(5, articleResult.Count());
        }

        [Fact]
        public async Task GetArticlesCountShouldReturnCorrectCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);

            var articles = this.ArticleSeeder(6, DateTime.UtcNow);

            foreach (var article in articles)
            {
                await repositoryArticle.AddAsync(article);
            }

            await repositoryArticle.SaveChangesAsync();

            AutoMapperConfig.RegisterMappings(typeof(GetAllArticleByIdTestViewModel).GetTypeInfo().Assembly);

            var service = new ArticleHomePageService(repositoryArticle);

            var articleResult = service.GetPostsCount();

            Assert.Equal(6, articleResult);
        }

        [Fact]
        public async Task GetLastThreeArticlesShouldReturnCorrectArticles()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);

            var articles = this.ArticleSeeder(4, DateTime.UtcNow);

            foreach (var article in articles)
            {
                await repositoryArticle.AddAsync(article);
            }

            await repositoryArticle.SaveChangesAsync();

            AutoMapperConfig.RegisterMappings(typeof(GetAllArticleByIdTestViewModel).GetTypeInfo().Assembly);

            var service = new ArticleHomePageService(repositoryArticle);

            var lastThreeArticlesResult = await service.LastThreePostsAsync<GetAllArticleByIdTestViewModel>();

            Assert.Equal(3, lastThreeArticlesResult.Count());
        }

        public IEnumerable<Article> ArticleSeeder(int id, DateTime createdOn)
        {
            List<Article> articles = new List<Article>();

            for (int i = 1; i <= id; i++)
            {
                var article = new Article
                {
                    Id = i,
                    Title = i.ToString(),
                    Content = i.ToString(),
                    CategoryId = i,
                    UserId = i.ToString(),
                    CreatedOn = createdOn,
                };

                articles.Add(article);

            }

            return articles;
        }
    }

    public class GetAllArticleByIdTestViewModel : IMapFrom<Article>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string CategoryId { get; set; }
    }
}
