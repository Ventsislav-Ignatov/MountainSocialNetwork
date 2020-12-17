namespace MountainSocialNetwork.Services.Data.Friend
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MountainSocialNetwork.Web.ViewModels.Friend;

    public interface IFriendService
    {
        Task<IEnumerable<T>> GetAllFriendRequestAsync<T>(string userId);

        Task CreateFriendRequestAsync(string senderId, string receiverId);

        Task DeleteFriendShipAsync(string loggedUserId, string userId);

        Task ApproveFriendRequestAsync(string senderId, string receiverId);

        Task DeclineFriendRequestAsync(string senderId, string receiverId);

        IEnumerable<UserFriendshipViewModel> GetAllFriendAsync(string userId);

        Task<bool> AlredyFriendOrSendFriendRequestAsync(string senderId, string receiverId);

        Task<int> RequestFriendCountAsync(string userId);

        Task<bool> AreTwoUsersFriendsAsync(string loginUserId, string friendId);
    }
}
