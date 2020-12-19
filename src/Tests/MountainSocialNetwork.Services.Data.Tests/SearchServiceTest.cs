using Microsoft.EntityFrameworkCore;
using MountainSocialNetwork.Data;
using MountainSocialNetwork.Data.Models;
using MountainSocialNetwork.Data.Repositories;
using MountainSocialNetwork.Services.Data.Search;
using MountainSocialNetwork.Services.Mapping;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace MountainSocialNetwork.Services.Data.Tests
{
    public class SearchServiceTest
    {
        [Fact]
        public async Task SearchShouldReturnCorrectCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);

            var article = new Article
            {
                Title = "test",
                Content = "test",
                UserId = "1",
                CategoryId = 1,
            };

            await repositoryArticle.AddAsync(article);

            await repositoryArticle.SaveChangesAsync();

            AutoMapperConfig.RegisterMappings(typeof(SearchServiceTestViewModel).GetTypeInfo().Assembly);

            var service = new SearchService(repositoryArticle);

            var result = await service.GetSearchedArticlesAsync<SearchServiceTestViewModel>("test");

            Assert.Equal(1, result.Count());

        }
    }

    public class SearchServiceTestViewModel : IMapFrom<Article>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public int CategoryId { get; set; }
    }
}
