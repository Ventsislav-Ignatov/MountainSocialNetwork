namespace MountainSocialNetwork.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MountainSocialNetwork.Data;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Data.Repositories;
    using MountainSocialNetwork.Services.Data.Friend;
    using MountainSocialNetwork.Services.Mapping;
    using Xunit;

    public class FriendServiceTest
    {
        [Fact]
        public async Task GetAllFriendRequestAsyncShouldReturnCorrectCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var dbContext = new ApplicationDbContext(options);

            var friendRepository = new EfDeletableEntityRepository<Friend>(dbContext);
            var friendRequestRepository = new EfDeletableEntityRepository<FriendRequest>(dbContext);

            var service = new FriendService(friendRequestRepository, friendRepository);

            var newFriendShip = new FriendRequest
            {
                SenderId = "1",
                ReceiverId = "2",
                Status = FriendRequestStatus.Pending,
            };

            await friendRequestRepository.AddAsync(newFriendShip);
            await friendRequestRepository.SaveChangesAsync();

            AutoMapperConfig.RegisterMappings(typeof(FriendRequestTestViewModel).GetTypeInfo().Assembly);

            var result = await service.GetAllFriendRequestAsync<FriendRequestTestViewModel>(newFriendShip.ReceiverId);

            Assert.Equal(1, result.Count());
        }

        [Fact]
        public async Task CreateFriendRequestAsyncShouldCreateCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var dbContext = new ApplicationDbContext(options);

            var friendRepository = new EfDeletableEntityRepository<Friend>(dbContext);
            var friendRequestRepository = new EfDeletableEntityRepository<FriendRequest>(dbContext);

            var service = new FriendService(friendRequestRepository, friendRepository);

            var result = service.CreateFriendRequestAsync("1", "2");

            Assert.Equal(1, friendRequestRepository.All().Count());
        }

        [Fact]
        public async Task AreTwoUsersFriendsAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var dbContext = new ApplicationDbContext(options);

            var friendRepository = new EfDeletableEntityRepository<Friend>(dbContext);
            var friendRequestRepository = new EfDeletableEntityRepository<FriendRequest>(dbContext);

            var service = new FriendService(friendRequestRepository, friendRepository);

            var newFriendShip = new Friend
            {
                SenderId = "1",
                ReceiverId = "2",
            };

            await friendRepository.AddAsync(newFriendShip);
            await friendRepository.SaveChangesAsync();

            var resultTrue = await service.AreTwoUsersFriendsAsync(newFriendShip.SenderId, newFriendShip.ReceiverId);
            var resultFalse = await service.AreTwoUsersFriendsAsync("4", "3");

            Assert.True(resultTrue);
            Assert.False(resultFalse);
        }

        [Fact]
        public async Task DeleteShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var dbContext = new ApplicationDbContext(options);

            var friendRepository = new EfDeletableEntityRepository<Friend>(dbContext);
            var friendRequestRepository = new EfDeletableEntityRepository<FriendRequest>(dbContext);

            var service = new FriendService(friendRequestRepository, friendRepository);

            var newFriendShip = new Friend
            {
                SenderId = "1",
                ReceiverId = "2",
            };

            await friendRepository.AddAsync(newFriendShip);
            await friendRepository.SaveChangesAsync();

            var result = service.DeleteFriendShipAsync(newFriendShip.SenderId, newFriendShip.ReceiverId);

            Assert.Equal(0, friendRepository.All().Count());
        }

        [Fact]
        public async Task GetPendingRequestForUserShouldReturnCorrectCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var dbContext = new ApplicationDbContext(options);

            var friendRepository = new EfDeletableEntityRepository<Friend>(dbContext);
            var friendRequestRepository = new EfDeletableEntityRepository<FriendRequest>(dbContext);

            var service = new FriendService(friendRequestRepository, friendRepository);

            var newFriendShip = new FriendRequest
            {
                SenderId = "1",
                ReceiverId = "2",
                Status = FriendRequestStatus.Pending,
            };

            await friendRequestRepository.AddAsync(newFriendShip);
            await friendRequestRepository.SaveChangesAsync();

            var result = await service.RequestFriendCountAsync("2");

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task AlredyFriendOrSendFriendRequestAsyncShouldReturnCorrectBool()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var dbContext = new ApplicationDbContext(options);

            var friendRepository = new EfDeletableEntityRepository<Friend>(dbContext);
            var friendRequestRepository = new EfDeletableEntityRepository<FriendRequest>(dbContext);

            var newFriendShip = new Friend
            {
                SenderId = "1",
                ReceiverId = "2",
            };

            var newFriendShipRequest = new FriendRequest
            {
                SenderId = "5",
                ReceiverId = "6",
                Status = FriendRequestStatus.Pending,
            };

            await friendRepository.AddAsync(newFriendShip);
            await friendRepository.SaveChangesAsync();

            await friendRequestRepository.AddAsync(newFriendShipRequest);
            await friendRequestRepository.SaveChangesAsync();

            var service = new FriendService(friendRequestRepository, friendRepository);

            var resultTrueAlreadyFriend = await service.AlredyFriendOrSendFriendRequestAsync("1", "2");

            var resultTrueSendFriendToHim = await service.AlredyFriendOrSendFriendRequestAsync("1", "1");

            var resultTrueAlredySendFriend = await service.AlredyFriendOrSendFriendRequestAsync("5", "6");

            var resultFalse = await service.AlredyFriendOrSendFriendRequestAsync("10", "11");

            Assert.True(resultTrueAlreadyFriend);
            Assert.True(resultTrueSendFriendToHim);
            Assert.True(resultTrueAlredySendFriend);
            Assert.False(resultFalse);
        }

        [Fact]
        public async Task DeclineFriendRequestAsyncShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var dbContext = new ApplicationDbContext(options);

            var friendRepository = new EfDeletableEntityRepository<Friend>(dbContext);
            var friendRequestRepository = new EfDeletableEntityRepository<FriendRequest>(dbContext);


            var newFriendShipRequest = new FriendRequest
            {
                SenderId = "5",
                ReceiverId = "6",
                Status = FriendRequestStatus.Pending,
            };


            await friendRequestRepository.AddAsync(newFriendShipRequest);
            await friendRequestRepository.SaveChangesAsync();

            var service = new FriendService(friendRequestRepository, friendRepository);

            var result = service.DeclineFriendRequestAsync("5", "6");

            var testResult = friendRequestRepository.All().Where(x => x.SenderId == "5" && x.Status == FriendRequestStatus.Declined).Count();

            Assert.Equal(1, testResult);
        }

        [Fact]

        public async Task ApproveShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var dbContext = new ApplicationDbContext(options);

            var friendRepository = new EfDeletableEntityRepository<Friend>(dbContext);
            var friendRequestRepository = new EfDeletableEntityRepository<FriendRequest>(dbContext);

            var newFriendShipRequest = new FriendRequest
            {
                SenderId = "1",
                ReceiverId = "2",
            };

            var newFriendShip = new Friend
            {
                SenderId = "1",
                ReceiverId = "2",
            };

            var service = new FriendService(friendRequestRepository, friendRepository);

            var sendRequestResult = service.CreateFriendRequestAsync("1", "2");

            var sendSecondRequestResult = service.CreateFriendRequestAsync("1", "2");

            var result = service.ApproveFriendRequestAsync("1", "2");

            var countResult = friendRepository.All().Count();

            Assert.Equal(1, countResult);

        }
    }

    public class FriendRequestTestViewModel : IMapFrom<FriendRequest>
    {
        public string SenderId { get; set; }

        public string ReceiverId { get; set; }
    }
}
