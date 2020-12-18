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

    public class CategoriesServiceTest
    {
        [Fact]
        public async Task GetAllCategoriesShouldReturnCorrectCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryCategory = new EfDeletableEntityRepository<Category>(dbContext);

            await repositoryCategory.AddAsync(new Category
            {
                Title = "test",
                Description = "test",
                Name = "test",
            });

            await repositoryCategory.SaveChangesAsync();

            AutoMapperConfig.RegisterMappings(typeof(CategoryTestViewMode).GetTypeInfo().Assembly);

            var service = new CategoriesService(repositoryCategory);

            var category = await service.GetAllAsync<CategoryTestViewMode>(1);

            Assert.Equal(1, category.Count());
        }

        [Fact]
        public async Task GetCategoriByNameShouldReturnCorrectCategory()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryCategory = new EfDeletableEntityRepository<Category>(dbContext);

            var category = new Category
            {
                Title = "test",
                Description = "test",
                Name = "test",
            };

            await repositoryCategory.AddAsync(category);

            await repositoryCategory.SaveChangesAsync();

            AutoMapperConfig.RegisterMappings(typeof(CategoryTestViewMode).GetTypeInfo().Assembly);

            var service = new CategoriesService(repositoryCategory);

            var categoryResult = await service.CategoriesByNameAsync<CategoryTestViewMode>(category.Name);

            Assert.Equal(category.Name, categoryResult.Name);
        }

        [Fact]
        public async Task CheckIfCategoryExitsByIdShouldReturnCorrectBool()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryCategory = new EfDeletableEntityRepository<Category>(dbContext);

            var category = new Category
            {
                Id = 1,
                Title = "test",
                Description = "test",
                Name = "test",
            };

            await repositoryCategory.AddAsync(category);

            await repositoryCategory.SaveChangesAsync();


            var service = new CategoriesService(repositoryCategory);

            var resultTrue = await service.CategoryExitsAsync(category.Id);
            var resultFalse = await service.CategoryExitsAsync(2);

            Assert.True(resultTrue);
            Assert.False(resultFalse);
        }
    }

    public class CategoryTestViewMode : IMapFrom<Category>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }
    }
}
