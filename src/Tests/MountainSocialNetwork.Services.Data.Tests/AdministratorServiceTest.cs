namespace MountainSocialNetwork.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MountainSocialNetwork.Data;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Data.Repositories;
    using MountainSocialNetwork.Services.Data.Administrator;
    using MountainSocialNetwork.Services.Mapping;
    using MountainSocialNetwork.Web.ViewModels;
    using Xunit;

    public class AdministratorServiceTest
    {
        public AdministratorServiceTest()
        {
        }

        [Fact]
        public async Task GetAllArticlesPostAsyncShouldReturnCorrectCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var repositoryNewsFeedComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var repositoryCategory = new EfDeletableEntityRepository<Category>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<Comment>(dbContext);

            var articleOne = new Article
            {
                Title = "Test",
                Content = "Test",
                UserId = "1",
                CategoryId = 1,
            };

            var articleTwo = new Article
            {
                Title = "Test",
                Content = "Test",
                UserId = "1",
                CategoryId = 2,
            };

            await repositoryArticle.AddAsync(articleOne);
            await repositoryArticle.AddAsync(articleTwo);
            await repositoryArticle.SaveChangesAsync();

            AutoMapperConfig.RegisterMappings(typeof(HomeBlogArticleViewModelTest).GetTypeInfo().Assembly);

            var service = new AdministratorService(repositoryNewsFeedPost, repositoryArticle, repositoryNewsFeedComment, repositoryCategory, repositoryComment);

            var result = await service.GetAllArticlesPostAsync<HomeBlogArticleViewModelTest>();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetAllNewsFeedPostAsyncShouldReturnCorrectCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var repositoryNewsFeedComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var repositoryCategory = new EfDeletableEntityRepository<Category>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<Comment>(dbContext);


            var postOne = new NewsFeedPost
            {
                Content = "Test",
                UserId = "1",
            };

            var postTwo = new NewsFeedPost
            {
                Content = "Test",
                UserId = "1",
            };

            await repositoryNewsFeedPost.AddAsync(postOne);
            await repositoryNewsFeedPost.AddAsync(postTwo);
            await repositoryNewsFeedPost.SaveChangesAsync();

            AutoMapperConfig.RegisterMappings(typeof(TimeLineAllPostsViewModelTest).GetTypeInfo().Assembly);

            var service = new AdministratorService(repositoryNewsFeedPost, repositoryArticle, repositoryNewsFeedComment, repositoryCategory, repositoryComment);

            var result = await service.GetAllNewsFeedPostAsync<TimeLineAllPostsViewModelTest>();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetAllNewsFeedCommentAsyncShouldReturnCorrectCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var repositoryNewsFeedComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var repositoryCategory = new EfDeletableEntityRepository<Category>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<Comment>(dbContext);


            var commentOne = new NewsFeedComment
            {
                Content = "Test",
                UserId = "1",
                NewsFeedPostId = 1,
            };

            var commentTwo = new NewsFeedComment
            {
                Content = "Test",
                UserId = "1",
                NewsFeedPostId = 1,
            };

            await repositoryNewsFeedComment.AddAsync(commentOne);
            await repositoryNewsFeedComment.AddAsync(commentTwo);
            await repositoryNewsFeedComment.SaveChangesAsync();


            AutoMapperConfig.RegisterMappings(typeof(NewsFeedPostCommentViewModelTest).GetTypeInfo().Assembly);

            var service = new AdministratorService(repositoryNewsFeedPost, repositoryArticle, repositoryNewsFeedComment, repositoryCategory, repositoryComment);

            var result = await service.GetAllNewsFeedCommentAsync<NewsFeedPostCommentViewModelTest>();

            Assert.Equal(2, result.Count());
        }

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

        [Fact]
        public async Task GetByIdNewsFeedPostAsyncShouldReturnCorrectPost()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var repositoryNewsFeedComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var repositoryCategory = new EfDeletableEntityRepository<Category>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<Comment>(dbContext);

            var newsFeedPost = new NewsFeedPost
            {
                Id = 1,
                Content = "test",
                UserId = "1",
            };
            await repositoryNewsFeedPost.AddAsync(newsFeedPost);

            await repositoryNewsFeedPost.SaveChangesAsync();

            AutoMapperConfig.RegisterMappings(typeof(NewsFeedPostByIdViewModelTest).GetTypeInfo().Assembly);

            var service = new AdministratorService(repositoryNewsFeedPost, repositoryArticle, repositoryNewsFeedComment, repositoryCategory, repositoryComment);

            var post = await service.GetByIdNewsFeedPostAsync<NewsFeedPostByIdViewModelTest>(newsFeedPost.Id);

            Assert.Equal(newsFeedPost.Id, post.Id);
        }

        [Fact]
        public async Task GetByIdArticleAsyncShouldReturnCorrectPost()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var repositoryNewsFeedComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var repositoryCategory = new EfDeletableEntityRepository<Category>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<Comment>(dbContext);

            var article = new Article
            {
                Id = 1,
                Title = "Test",
                Content = "Test",
                UserId = "1",
                CategoryId = 1,
            };

            await repositoryArticle.AddAsync(article);

            await repositoryArticle.SaveChangesAsync();

            AutoMapperConfig.RegisterMappings(typeof(ArticleByIdViewModelTest).GetTypeInfo().Assembly);

            var service = new AdministratorService(repositoryNewsFeedPost, repositoryArticle, repositoryNewsFeedComment, repositoryCategory, repositoryComment);

            var post = await service.GetByIdArticleAsync<ArticleByIdViewModelTest>(article.Id);

            Assert.Equal(article.Id, post.Id);
        }

        [Fact]
        public async Task GetByIdCommentAsyncShouldReturnCorrectComment()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var repositoryNewsFeedComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var repositoryCategory = new EfDeletableEntityRepository<Category>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<Comment>(dbContext);

            var commentOne = new NewsFeedComment
            {
                Content = "Test",
                UserId = "1",
                NewsFeedPostId = 1,
            };

            await repositoryNewsFeedComment.AddAsync(commentOne);
            await repositoryNewsFeedComment.SaveChangesAsync();

            AutoMapperConfig.RegisterMappings(typeof(NewsFeedPostCommentViewModelTest).GetTypeInfo().Assembly);

            var service = new AdministratorService(repositoryNewsFeedPost, repositoryArticle, repositoryNewsFeedComment, repositoryCategory, repositoryComment);

            var comment = service.GetByIdCommentAsync<NewsFeedPostCommentViewModelTest>(commentOne.Id);

            Assert.Equal(commentOne.Id, comment.Id);
        }

        [Fact]
        public async Task GetAllCategoriesAsyncShouldReturnCorrectCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var repositoryNewsFeedComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var repositoryCategory = new EfDeletableEntityRepository<Category>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<Comment>(dbContext);

            var categoryOne = new Category
            {
                Title = "test",
                Name = "test",
                Description = "TEST",
            };

            var categoryTwo = new Category
            {
                Title = "test",
                Name = "test",
                Description = "TEST",
            };

            await repositoryCategory.AddAsync(categoryOne);
            await repositoryCategory.AddAsync(categoryTwo);
            await repositoryCategory.SaveChangesAsync();

            AutoMapperConfig.RegisterMappings(typeof(CategoryTestViewModel).GetTypeInfo().Assembly);

            var service = new AdministratorService(repositoryNewsFeedPost, repositoryArticle, repositoryNewsFeedComment, repositoryCategory, repositoryComment);

            var category = await service.GetAllCategoriesAsync<CategoryTestViewModel>();

            Assert.Equal(2, category.Count());
        }

        [Fact]
        public async Task GetAllArticlesCommentAsyncShouldReturnCorrectCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryArticle = new EfDeletableEntityRepository<Article>(dbContext);
            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var repositoryNewsFeedComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var repositoryCategory = new EfDeletableEntityRepository<Category>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<Comment>(dbContext);

            var comment = new Comment
            {
                ArticleId = 1,
                Content = "test",
                UserId = "1",
            };

            await repositoryComment.AddAsync(comment);
            await repositoryComment.SaveChangesAsync();

            AutoMapperConfig.RegisterMappings(typeof(CommentTestViewModel).GetTypeInfo().Assembly);

            var service = new AdministratorService(repositoryNewsFeedPost, repositoryArticle, repositoryNewsFeedComment, repositoryCategory, repositoryComment);

            var comments = await service.GetAllArticlesCommentAsync<CommentTestViewModel>();

            Assert.Equal(1, comments.Count());
        }
    }

    public class CommentTestViewModel : IMapFrom<Comment>
    {
        public int ArticleId { get; set; }

        public string UserId { get; set; }

        public string Content { get; set; }
    }

    public class CategoryTestViewModel : IMapFrom<Category>
    {
        public string Title { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }
    }

    public class ArticleByIdViewModelTest : IMapFrom<Article>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string CategoryId { get; set; }
    }

    public class NewsFeedPostByIdViewModelTest : IMapFrom<NewsFeedPost>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }
    }

    public class HomeBlogArticleViewModelTest : IMapFrom<Article>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }

    public class TimeLineAllPostsViewModelTest : IMapFrom<NewsFeedPost>
    {
        public string Content { get; set; }

        public string UserId { get; set; }
    }

    public class NewsFeedPostCommentViewModelTest : IMapFrom<NewsFeedComment>
    {
        public string Content { get; set; }

        public string UserId { get; set; }

        public int NewsFeedPostId { get; set; }
    }
}
