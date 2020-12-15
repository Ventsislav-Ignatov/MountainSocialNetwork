namespace MountainSocialNetwork.Services.Data.Friend
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MountainSocialNetwork.Data.Common.Repositories;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;
    using MountainSocialNetwork.Web.ViewModels.Friend;

    public class FriendService : IFriendService
    {
        private readonly IDeletableEntityRepository<FriendRequest> friendRequestRepository;
        private readonly IDeletableEntityRepository<Friend> friendRepository;

        public FriendService(IDeletableEntityRepository<FriendRequest> friendRequestRepository, IDeletableEntityRepository<Friend> friendRepository)
        {
            this.friendRequestRepository = friendRequestRepository;
            this.friendRepository = friendRepository;
        }

        public async Task<IEnumerable<T>> GetAllFriendRequestAsync<T>(string userId)
        {
            var friendRequests = await this.friendRequestRepository.All()
                .Where(x => x.Status == FriendRequestStatus.Pending && x.ReceiverId == userId)
                .To<T>()
                .ToListAsync();

            return friendRequests;
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

        public IEnumerable<UserFriendshipViewModel> GetAllFriendAsync(string userId)
        {
            List<UserFriendshipViewModel> friends = null;

            if (this.friendRepository.All().Any(x => x.ReceiverId == userId))
            {
                friends = this.friendRepository.All().Where(x => x.ReceiverId == userId)
                    .Select(x => new UserFriendshipViewModel
                    {
                        FirstName = x.Sender.FirstName,
                        LastName = x.Sender.LastName,
                        PictureURL = x.Sender.UserProfilePictures.OrderByDescending(a => a.CreatedOn).FirstOrDefault().PictureURL,
                    }).ToList();
            }

            if (this.friendRepository.All().Any(x => x.SenderId == userId))
            {
                List<UserFriendshipViewModel> friendsTwo = null;

                friendsTwo = this.friendRepository.All().Where(x => x.SenderId == userId)
                  .Select(x => new UserFriendshipViewModel
                  {
                      FirstName = x.Sender.FirstName,
                      LastName = x.Sender.LastName,
                      PictureURL = x.Sender.UserProfilePictures.OrderByDescending(a => a.CreatedOn).FirstOrDefault().PictureURL,
                  }).ToList();

                foreach (var frien in friendsTwo)
                {
                    friends.Add(frien);
                }
            }

            return friends;
        }

        public async Task<bool> AlredyFriend(string senderId, string receiverId)
        {
            var alredyFriend = await this.friendRepository.All()
                .Where(x => x.SenderId == senderId && x.ReceiverId == receiverId)
                .FirstOrDefaultAsync();

            if (alredyFriend != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
