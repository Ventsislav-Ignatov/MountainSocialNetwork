namespace MountainSocialNetwork.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using MountainSocialNetwork.Data;
    using MountainSocialNetwork.Data.Common.Repositories;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Data.Repositories;
    using MountainSocialNetwork.Services.Data.NewsFeed;
    using MountainSocialNetwork.Services.Mapping;
    using MountainSocialNetwork.Web.ViewModels.BlogPosts;
    using MountainSocialNetwork.Web.ViewModels.NewsFeed;
    using MountainSocialNetwork.Web.ViewModels.UsersPosts;
    using Xunit;

    public class NewsFeedServiceTest
    {
        [Fact]
        public async Task CreateShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var applicationUser = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var pictureRepository = new EfRepository<UserProfilePicture>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var coverPictureRepository = new EfRepository<UserCoverPicture>(dbContext);
            var friendRepository = new EfDeletableEntityRepository<Friend>(dbContext);

            var post = new NewsFeedPost
            {
                Content = "test",
                UserId = "1",
            };

            var service = new NewsFeedService(repositoryNewsFeedPost, applicationUser, pictureRepository, repositoryComment, coverPictureRepository, friendRepository);

            var postResult = await service.CreateAsync(post.Content, post.UserId);

            Assert.Equal(1, repositoryNewsFeedPost.All().Count());
        }

        [Fact]
        public async Task GetByIdShouldReturnCorrectPost()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var applicationUser = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var pictureRepository = new EfRepository<UserProfilePicture>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var coverPictureRepository = new EfRepository<UserCoverPicture>(dbContext);
            var friendRepository = new EfDeletableEntityRepository<Friend>(dbContext);

            var post = new NewsFeedPost
            {
                Id = 1,
                Content = "test",
                UserId = "1",
            };

            await repositoryNewsFeedPost.AddAsync(post);

            await repositoryNewsFeedPost.SaveChangesAsync();

            var service = new NewsFeedService(repositoryNewsFeedPost, applicationUser, pictureRepository, repositoryComment, coverPictureRepository, friendRepository);

            AutoMapperConfig.RegisterMappings(typeof(GetByIdTestViewModel).GetTypeInfo().Assembly);

            var postResult = await service.GetByIdAsync<GetByIdTestViewModel>(post.Id);

            Assert.Equal(post.Id, postResult.Id);
        }

        [Fact]
        public async Task GetAllNewsFeedPostPerPageShouldReturnCorrectCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var applicationUser = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var pictureRepository = new EfRepository<UserProfilePicture>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var coverPictureRepository = new EfRepository<UserCoverPicture>(dbContext);
            var friendRepository = new EfDeletableEntityRepository<Friend>(dbContext);

            var posts = this.NewsFeedPostSeeder(5, DateTime.UtcNow);

            foreach (var post in posts)
            {

                await repositoryNewsFeedPost.AddAsync(post);

            }

            await repositoryNewsFeedPost.SaveChangesAsync();

            var service = new NewsFeedService(repositoryNewsFeedPost, applicationUser, pictureRepository, repositoryComment, coverPictureRepository, friendRepository);

            AutoMapperConfig.RegisterMappings(typeof(GetAllNewsFeedPostPerPageTestViewModel).GetTypeInfo().Assembly);

            var postResult = service.GetAllSocialPosts<GetAllNewsFeedPostPerPageTestViewModel>(1);

            Assert.Equal(4, postResult.Count());
        }

        [Fact]
        public async Task ShouldReturnIfPostExistAndUserIsCorrectOwner()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var applicationUser = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var pictureRepository = new EfRepository<UserProfilePicture>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var coverPictureRepository = new EfRepository<UserCoverPicture>(dbContext);
            var friendRepository = new EfDeletableEntityRepository<Friend>(dbContext);

            var post = new NewsFeedPost
            {
                Id = 1,
                Content = "test",
                UserId = "1",
            };

            await repositoryNewsFeedPost.AddAsync(post);

            await repositoryNewsFeedPost.SaveChangesAsync();

            var service = new NewsFeedService(repositoryNewsFeedPost, applicationUser, pictureRepository, repositoryComment, coverPictureRepository, friendRepository);

            AutoMapperConfig.RegisterMappings(typeof(GetByIdTestViewModel).GetTypeInfo().Assembly);

            var result = await service.ExistsAndOwnerAsync(1, "1");

            Assert.True(result);
        }

        [Fact]

        public async Task UpdateShouldWorkCorrectly()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var applicationUser = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var pictureRepository = new EfRepository<UserProfilePicture>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var coverPictureRepository = new EfRepository<UserCoverPicture>(dbContext);
            var friendRepository = new EfDeletableEntityRepository<Friend>(dbContext);

            var post = new NewsFeedPost
            {
                Id = 1,
                Content = "test",
                UserId = "1",
            };

            var newPost = new NewsFeedPost
            {
                Id = 1,
                Content = "UpdatedTest",
                UserId = "1",
            };

            await repositoryNewsFeedPost.AddAsync(post);

            await repositoryNewsFeedPost.SaveChangesAsync();

            var service = new NewsFeedService(repositoryNewsFeedPost, applicationUser, pictureRepository, repositoryComment, coverPictureRepository, friendRepository);

            AutoMapperConfig.RegisterMappings(typeof(GetByIdTestViewModel).GetTypeInfo().Assembly);

            var result = service.UpdateAsync(newPost);

            Assert.Equal(newPost.Content, newPost.Content);
        }

        [Fact]
        public async Task DeleteShouldDeleteCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
          .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var applicationUser = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var pictureRepository = new EfRepository<UserProfilePicture>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var coverPictureRepository = new EfRepository<UserCoverPicture>(dbContext);
            var friendRepository = new EfDeletableEntityRepository<Friend>(dbContext);

            var post = new NewsFeedPost
            {
                Id = 1,
                Content = "test",
                UserId = "1",
            };


            await repositoryNewsFeedPost.AddAsync(post);

            await repositoryNewsFeedPost.SaveChangesAsync();

            var service = new NewsFeedService(repositoryNewsFeedPost, applicationUser, pictureRepository, repositoryComment, coverPictureRepository, friendRepository);

            AutoMapperConfig.RegisterMappings(typeof(GetByIdTestViewModel).GetTypeInfo().Assembly);

            var result = service.DeleteAsync(post);

            Assert.Equal(0, repositoryNewsFeedPost.All().Count());
        }

        [Fact]
        public async Task ShouldReturnCorrectPostById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var applicationUser = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var pictureRepository = new EfRepository<UserProfilePicture>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var coverPictureRepository = new EfRepository<UserCoverPicture>(dbContext);
            var friendRepository = new EfDeletableEntityRepository<Friend>(dbContext);

            var post = new NewsFeedPost
            {
                Id = 1,
                Content = "test",
                UserId = "1",
            };

            await repositoryNewsFeedPost.AddAsync(post);

            await repositoryNewsFeedPost.SaveChangesAsync();

            var service = new NewsFeedService(repositoryNewsFeedPost, applicationUser, pictureRepository, repositoryComment, coverPictureRepository, friendRepository);

            AutoMapperConfig.RegisterMappings(typeof(GetByIdTestViewModel).GetTypeInfo().Assembly);

            var result = service.GetNewsFeedPostAsync(post.Id);

            Assert.Equal(post.Id, result.Id);
        }

        [Fact]
        public async Task ShouldReturnAllSocialNewsFeedPostByUserAndCorrectPostPerPage()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var applicationUser = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var pictureRepository = new EfRepository<UserProfilePicture>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var coverPictureRepository = new EfRepository<UserCoverPicture>(dbContext);
            var friendRepository = new EfDeletableEntityRepository<Friend>(dbContext);

            var posts = this.NewsFeedPostSeeder(5, DateTime.UtcNow);

            foreach (var post in posts)
            {

                await repositoryNewsFeedPost.AddAsync(post);

            }

            await repositoryNewsFeedPost.SaveChangesAsync();

            var service = new NewsFeedService(repositoryNewsFeedPost, applicationUser, pictureRepository, repositoryComment, coverPictureRepository, friendRepository);

            AutoMapperConfig.RegisterMappings(typeof(GetAllNewsFeedPostPerPageTestViewModel).GetTypeInfo().Assembly);

            var postResult = service.GetAllSocialPostsByUser<GetAllNewsFeedPostPerPageTestViewModel>("1", 1);

            Assert.Equal(1, postResult.Count());
        }

        [Fact]

        public async Task UserInfoUpdateShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
       .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var applicationUser = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var pictureRepository = new EfRepository<UserProfilePicture>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var coverPictureRepository = new EfRepository<UserCoverPicture>(dbContext);
            var friendRepository = new EfDeletableEntityRepository<Friend>(dbContext);

            var user = new ApplicationUser
            {
                Id = "1",
                FirstName = "Ventsi",
                LastName = "Ignatov",
                Email = "Ventsi@gmail.com",
                UserName = "Ventsislav@gmail.com",
                BirthDay = DateTime.UtcNow,
                Town = "Barcelona",
            };

            var userWithUpdatedInfo = new ApplicationUser
            {
                Id = "1",
                FirstName = "Ventsislav",
                LastName = "Ignatov",
                Email = "Ventsi@gmail.com",
                UserName = "Ventsislav@gmail.com",
                BirthDay = DateTime.UtcNow,
                Town = "Madrid",
            };


            await applicationUser.AddAsync(user);

            await applicationUser.SaveChangesAsync();

            var service = new NewsFeedService(repositoryNewsFeedPost, applicationUser, pictureRepository, repositoryComment, coverPictureRepository, friendRepository);

            var result = service.EditProfileAsync(userWithUpdatedInfo, user.Id);

        }

        public IEnumerable<NewsFeedPost> NewsFeedPostSeeder(int id, DateTime createdOn)
        {
            List<NewsFeedPost> newsFeedPosts = new List<NewsFeedPost>();

            for (int i = 1; i <= id; i++)
            {
                var newsFeedPost = new NewsFeedPost
                {
                    Id = i,
                    Content = i.ToString(),
                    UserId = i.ToString(),
                    CreatedOn = createdOn,
                };

                newsFeedPosts.Add(newsFeedPost);

            }

            return newsFeedPosts;
        }

        [Fact]
        public async Task CreateProfilePictureAsyncShouldCreateCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
       .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var applicationUser = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var pictureRepository = new EfRepository<UserProfilePicture>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var coverPictureRepository = new EfRepository<UserCoverPicture>(dbContext);
            var friendRepository = new EfDeletableEntityRepository<Friend>(dbContext);

            var userPicture = new UserProfilePicture
            {
                PictureURL = "mountain.jpg",
                ApplicationUserId = "1",
            };

            var service = new NewsFeedService(repositoryNewsFeedPost, applicationUser, pictureRepository, repositoryComment, coverPictureRepository, friendRepository);

            var createResult = service.CreateProfilePictureAsync(userPicture.ApplicationUserId, userPicture.PictureURL);

            Assert.Equal(1, pictureRepository.All().Count());
        }

        [Fact]
        public async Task CreateCoverPictureAsyncShouldCreateCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
       .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var applicationUser = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var pictureRepository = new EfRepository<UserProfilePicture>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var coverPictureRepository = new EfRepository<UserCoverPicture>(dbContext);
            var friendRepository = new EfDeletableEntityRepository<Friend>(dbContext);

            var userPicture = new UserCoverPicture
            {
                PictureURL = "mountain.jpg",
                ApplicationUserId = "1",
            };

            var service = new NewsFeedService(repositoryNewsFeedPost, applicationUser, pictureRepository, repositoryComment, coverPictureRepository, friendRepository);

            var createResult = service.CreateCoverPictureAsync(userPicture.ApplicationUserId, userPicture.PictureURL);

            Assert.Equal(1, coverPictureRepository.All().Count());
        }

        [Fact]
        public async Task GetLastProfilePictureShouldReturnLastPicture()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
       .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var applicationUser = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var pictureRepository = new EfRepository<UserProfilePicture>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var coverPictureRepository = new EfRepository<UserCoverPicture>(dbContext);
            var friendRepository = new EfDeletableEntityRepository<Friend>(dbContext);

            var firstUserPicture = new UserProfilePicture
            {
                PictureURL = "mountain.jpg",
                ApplicationUserId = "1",
            };

            var secondUserPicture = new UserProfilePicture
            {
                PictureURL = "mountain2.jpg",
                ApplicationUserId = "1",
            };

            await pictureRepository.AddAsync(firstUserPicture);
            await pictureRepository.AddAsync(secondUserPicture);
            await pictureRepository.SaveChangesAsync();

            var service = new NewsFeedService(repositoryNewsFeedPost, applicationUser, pictureRepository, repositoryComment, coverPictureRepository, friendRepository);

            var result = await service.LastProfilePictureAsync("1");
            var resultWhenIsNull = await service.LastProfilePictureAsync("2");

            Assert.Equal(secondUserPicture.PictureURL, result);
            Assert.Null(resultWhenIsNull);
        }

        [Fact]
        public async Task GetLastCoverPictureShouldReturnLastPicture()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
       .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var applicationUser = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var pictureRepository = new EfRepository<UserProfilePicture>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var coverPictureRepository = new EfRepository<UserCoverPicture>(dbContext);
            var friendRepository = new EfDeletableEntityRepository<Friend>(dbContext);

            var firstUserPicture = new UserCoverPicture
            {
                PictureURL = "mountain.jpg",
                ApplicationUserId = "1",
            };

            var secondUserPicture = new UserCoverPicture
            {
                PictureURL = "mountain2.jpg",
                ApplicationUserId = "1",
            };

            await coverPictureRepository.AddAsync(firstUserPicture);
            await coverPictureRepository.AddAsync(secondUserPicture);
            await coverPictureRepository.SaveChangesAsync();

            var service = new NewsFeedService(repositoryNewsFeedPost, applicationUser, pictureRepository, repositoryComment, coverPictureRepository, friendRepository);

            var result = await service.LastCoverPictureAsync("1");
            var resultWhenIsNull = await service.LastCoverPictureAsync("2");

            Assert.Equal(secondUserPicture.PictureURL, result);
            Assert.Null(resultWhenIsNull);
        }

        [Fact]
        public void GetPostsCountShouldReturnCorrectCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var applicationUser = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var pictureRepository = new EfRepository<UserProfilePicture>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var coverPictureRepository = new EfRepository<UserCoverPicture>(dbContext);
            var friendRepository = new EfDeletableEntityRepository<Friend>(dbContext);

            var post = new NewsFeedPost
            {
                Content = "test",
                UserId = "1",
            };

            var secondPost = new NewsFeedPost
            {
                Content = "testTwo",
                UserId = "2",
            };

            var service = new NewsFeedService(repositoryNewsFeedPost, applicationUser, pictureRepository, repositoryComment, coverPictureRepository, friendRepository);

            var postResult = service.CreateAsync(post.Content, post.UserId);

            var secondPostResult = service.CreateAsync(secondPost.Content, secondPost.UserId);

            var postCount = service.GetPostsCount();

            Assert.Equal(2, postCount);
        }

        [Fact]
        public void GetPostsCountByUserShouldReturnCorrectCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var applicationUser = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var pictureRepository = new EfRepository<UserProfilePicture>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var coverPictureRepository = new EfRepository<UserCoverPicture>(dbContext);
            var friendRepository = new EfDeletableEntityRepository<Friend>(dbContext);

            var post = new NewsFeedPost
            {
                Content = "test",
                UserId = "1",
            };

            var secondPost = new NewsFeedPost
            {
                Content = "testTwo",
                UserId = "1",
            };

            var service = new NewsFeedService(repositoryNewsFeedPost, applicationUser, pictureRepository, repositoryComment, coverPictureRepository, friendRepository);

            var postResult = service.CreateAsync(post.Content, post.UserId);

            var secondPostResult = service.CreateAsync(secondPost.Content, secondPost.UserId);

            var postCount = service.GetPostsCountByUser(post.UserId);

            Assert.Equal(2, postCount);
        }

        //[Fact]
        //public async Task GetAllCommentShouldReturnCorrectCount()
        //{
        //    var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        //     .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        //    var dbContext = new ApplicationDbContext(options);

        //    var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
        //    var applicationUser = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
        //    var pictureRepository = new EfRepository<UserProfilePicture>(dbContext);
        //    var repositoryComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
        //    var coverPictureRepository = new EfRepository<UserCoverPicture>(dbContext);
        //    var friendRepository = new EfDeletableEntityRepository<Friend>(dbContext);

        //    var firstComment = new NewsFeedComment
        //    {
        //        NewsFeedPostId = 1,
        //        UserId = "1",
        //        Content = "Test",
        //    };

        //    var secondComment = new NewsFeedComment
        //    {
        //        NewsFeedPostId = 1,
        //        UserId = "1",
        //        Content = "TestTwo",
        //    };

        //    await repositoryComment.AddAsync(firstComment);
        //    await repositoryComment.AddAsync(secondComment);
        //    await repositoryComment.SaveChangesAsync();

        //    var service = new NewsFeedService(repositoryNewsFeedPost, applicationUser, pictureRepository, repositoryComment, coverPictureRepository, friendRepository);

        //    IEnumerable<PostCommentViewModel> result = await service.GetAllCommentsAsync();

        //    Assert.Equal(2, result.Count());
        //}

        [Fact]
        public async Task GetAllProfilePicturesAsyncShouldReturnCorrectCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
       .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var applicationUser = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var pictureRepository = new EfRepository<UserProfilePicture>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var coverPictureRepository = new EfRepository<UserCoverPicture>(dbContext);
            var friendRepository = new EfDeletableEntityRepository<Friend>(dbContext);

            var userPicture = new UserProfilePicture
            {
                PictureURL = "mountain.jpg",
                ApplicationUserId = "1",
            };

            var secondUserPicture = new UserProfilePicture
            {
                PictureURL = "mountainTwo.jpg",
                ApplicationUserId = "1",
            };

            var service = new NewsFeedService(repositoryNewsFeedPost, applicationUser, pictureRepository, repositoryComment, coverPictureRepository, friendRepository);

            var firstPictureResult = service.CreateProfilePictureAsync(userPicture.ApplicationUserId, userPicture.PictureURL);
            var secondPictureResult = service.CreateProfilePictureAsync(secondUserPicture.ApplicationUserId, secondUserPicture.PictureURL);

            AutoMapperConfig.RegisterMappings(typeof(GetAllProfilePictureForUserViewModel).GetTypeInfo().Assembly);

            var resultCount = await service.GetAllProfilePicturesAsync<GetAllProfilePictureForUserViewModel>(userPicture.ApplicationUserId);

            Assert.Equal(2, resultCount.Count());
        }

        [Fact]
        public async Task GetAllCoverPicturesAsyncShouldReturnCorrectCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
       .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var applicationUser = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var pictureRepository = new EfRepository<UserProfilePicture>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var coverPictureRepository = new EfRepository<UserCoverPicture>(dbContext);
            var friendRepository = new EfDeletableEntityRepository<Friend>(dbContext);

            var userPicture = new UserCoverPicture
            {
                PictureURL = "mountain.jpg",
                ApplicationUserId = "1",
            };

            var secondUserPicture = new UserCoverPicture
            {
                PictureURL = "mountainTwo.jpg",
                ApplicationUserId = "1",
            };

            var service = new NewsFeedService(repositoryNewsFeedPost, applicationUser, pictureRepository, repositoryComment, coverPictureRepository, friendRepository);

            var firstPictureResult = service.CreateCoverPictureAsync(userPicture.ApplicationUserId, userPicture.PictureURL);
            var secondPictureResult = service.CreateCoverPictureAsync(secondUserPicture.ApplicationUserId, secondUserPicture.PictureURL);

            AutoMapperConfig.RegisterMappings(typeof(GetAllCoverPictureForUserViewModel).GetTypeInfo().Assembly);

            var resultCount = await service.GetAllCoverPicturesAsync<GetAllCoverPictureForUserViewModel>(userPicture.ApplicationUserId);

            Assert.Equal(2, resultCount.Count());
        }

        [Fact]
        public async Task GetFriendsForUserShouldReturnCorrectCount()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
       .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repositoryNewsFeedPost = new EfDeletableEntityRepository<NewsFeedPost>(dbContext);
            var applicationUser = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var pictureRepository = new EfRepository<UserProfilePicture>(dbContext);
            var repositoryComment = new EfDeletableEntityRepository<NewsFeedComment>(dbContext);
            var coverPictureRepository = new EfRepository<UserCoverPicture>(dbContext);
            var friendRepository = new EfDeletableEntityRepository<Friend>(dbContext);

            var firstFiend = new Friend
            {
                SenderId = "1",
                ReceiverId = "2",
            };

            var secondFriend = new Friend
            {
                SenderId = "1",
                ReceiverId = "4",
            };

            await friendRepository.AddAsync(firstFiend);
            await friendRepository.AddAsync(secondFriend);
            await friendRepository.SaveChangesAsync();

            var service = new NewsFeedService(repositoryNewsFeedPost, applicationUser, pictureRepository, repositoryComment, coverPictureRepository, friendRepository);

            var result = await service.GetFriendCountAsync(firstFiend.SenderId);

            Assert.Equal(2, result);
        }
    }

    public class GetAllCoverPictureForUserViewModel : IMapFrom<UserCoverPicture>
    {
        public string ApplicationUserId { get; set; }

        public string PictureURL { get; set; }
    }

    public class GetAllProfilePictureForUserViewModel : IMapFrom<UserProfilePicture>
    {
        public string ApplicationUserId { get; set; }

        public string PictureURL { get; set; }
    }

    public class GetAllNewsFeedPostPerPageTestViewModel : IMapFrom<NewsFeedPost>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }
    }

    public class GetByIdTestViewModel : IMapFrom<NewsFeedPost>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }
    }
}
