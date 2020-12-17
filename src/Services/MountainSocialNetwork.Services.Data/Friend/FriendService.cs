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
        private List<UserFriendshipViewModel> senderFriends;

        private List<UserFriendshipViewModel> receiverFriends;

        public FriendService(IDeletableEntityRepository<FriendRequest> friendRequestRepository, IDeletableEntityRepository<Friend> friendRepository)
        {
            this.friendRequestRepository = friendRequestRepository;
            this.friendRepository = friendRepository;
            this.senderFriends = new List<UserFriendshipViewModel>();
            this.receiverFriends = new List<UserFriendshipViewModel>();
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

            var hasAlredySendFriendShip = await this.friendRequestRepository.All()
                .Where(x => (x.SenderId == senderId && x.ReceiverId == receiverId) || (x.SenderId == receiverId && x.ReceiverId == senderId))
                .FirstOrDefaultAsync(a => a.IsDeleted == false);

            if (hasAlredySendFriendShip != null)
            {
                this.friendRequestRepository.Delete(hasAlredySendFriendShip);
            }

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

            if (this.friendRepository.All().Any(x => x.ReceiverId == userId))
            {
                this.senderFriends = this.friendRepository.All().Where(x => x.ReceiverId == userId)
                    .Select(x => new UserFriendshipViewModel
                    {
                        FirstName = x.Sender.FirstName,
                        LastName = x.Sender.LastName,
                        PictureURL = x.Sender.UserProfilePictures.OrderByDescending(a => a.CreatedOn).FirstOrDefault().PictureURL,
                    }).ToList();
            }

            if (this.friendRepository.All().Any(x => x.SenderId == userId))
            {

                this.receiverFriends = this.friendRepository.All().Where(x => x.SenderId == userId)
                  .Select(x => new UserFriendshipViewModel
                  {
                      FirstName = x.Receiver.FirstName,
                      LastName = x.Receiver.LastName,
                      PictureURL = x.Receiver.UserProfilePictures.OrderByDescending(a => a.CreatedOn).FirstOrDefault().PictureURL,
                  }).ToList();

                foreach (var currentFriend in this.receiverFriends)
                {

                    this.senderFriends.Add(currentFriend);
                }
            }

            return this.senderFriends;
        }

        public async Task<bool> AlredyFriendOrSendFriendRequestAsync(string senderId, string receiverId)
        {
            var alredyFriend = await this.friendRepository.All()
                                                          .Where(x => x.SenderId == senderId && x.ReceiverId == receiverId)
                                                          .FirstOrDefaultAsync();

            var alredySendFriendRequest = await this.friendRequestRepository.All()
                    .Where(x => (x.SenderId == senderId && x.ReceiverId == receiverId && x.Status == FriendRequestStatus.Pending) || (x.SenderId == receiverId && x.ReceiverId == senderId && x.Status == FriendRequestStatus.Pending))
                     .FirstOrDefaultAsync();

            if (alredyFriend != null)
            {
                return true;
            }
            else if (alredySendFriendRequest != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<int> RequestFriendCountAsync(string userId)
        {
            var count = await this.friendRequestRepository.All()
                                                    .Where(x => x.ReceiverId == userId)
                                                    .Where(a => a.Status == FriendRequestStatus.Pending)
                                                    .CountAsync();

            return count;
        }

        public async Task<bool> AreTwoUsersFriendsAsync(string loginUserId, string friendId)
        {
            var isFriend = await this.friendRepository.All().Where(x => (x.SenderId == loginUserId && x.ReceiverId == friendId) || (x.SenderId == friendId && x.ReceiverId == loginUserId))
                .FirstOrDefaultAsync();

            if (isFriend != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task DeleteFriendShipAsync(string loggedUserId, string userId)
        {
            var friend = await this.friendRepository.All()
                .Where(x => (x.ReceiverId == loggedUserId && x.SenderId == userId) || (x.SenderId == loggedUserId && x.ReceiverId == userId))
                .FirstOrDefaultAsync();

            this.friendRepository.Delete(friend);

            await this.friendRepository.SaveChangesAsync();
        }
    }
}
