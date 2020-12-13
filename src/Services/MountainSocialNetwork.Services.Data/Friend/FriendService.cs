namespace MountainSocialNetwork.Services.Data.Friend
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MountainSocialNetwork.Data.Common.Repositories;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class FriendService : IFriendService
    {
        private readonly IDeletableEntityRepository<FriendRequest> friendRequestRepository;
        private readonly IDeletableEntityRepository<Friend> friendRepository;

        public FriendService(IDeletableEntityRepository<FriendRequest> friendRequestRepository, IDeletableEntityRepository<Friend> friendRepository)
        {
            this.friendRequestRepository = friendRequestRepository;
            this.friendRepository = friendRepository;
        }

        public async Task CreateFriendRequestAsync(string senderId, string receiverId)
        {
            var newFriendRequest = new FriendRequest
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Status = FriendRequestStatus.Pending,
            };

            await this.friendRequestRepository.AddAsync(newFriendRequest);
            await this.friendRequestRepository.SaveChangesAsync();
        }

        public async Task ApproveFriendRequestAsync(string senderId, string receiverId)
        {
            var friendRequest = await this.friendRequestRepository
                .All()
                .FirstOrDefaultAsync(x => x.SenderId == senderId && x.ReceiverId == receiverId && x.Status == FriendRequestStatus.Pending);

            friendRequest.Status = FriendRequestStatus.Accepted;

            var newFriendShip = new Friend
            {
                SenderId = senderId,
                ReceiverId = receiverId,
            };

            await this.friendRepository.AddAsync(newFriendShip);

            await this.friendRequestRepository.SaveChangesAsync();
        }

        public async Task DeclineFriendRequestAsync(string senderId, string receiverId)
        {
            var friendRequest = await this.friendRequestRepository
              .All()
              .FirstOrDefaultAsync(x => x.SenderId == senderId && x.ReceiverId == receiverId && x.Status == FriendRequestStatus.Pending);

            friendRequest.Status = FriendRequestStatus.Declined;

            await this.friendRequestRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllFriendRequestAsync<T>(string userId)
        {
            var friendRequests = await this.friendRequestRepository.All()
                .Where(x => x.Status == FriendRequestStatus.Pending && x.ReceiverId == userId)
                .To<T>()
                .ToListAsync();

            return friendRequests;
        }
    }
}
