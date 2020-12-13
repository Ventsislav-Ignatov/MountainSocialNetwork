namespace MountainSocialNetwork.Services.Data.Friend
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IFriendService
    {
        Task CreateFriendRequestAsync(string senderId, string receiverId);

        Task ApproveFriendRequestAsync(string senderId, string receiverId);

        Task DeclineFriendRequestAsync(string senderId, string receiverId);

        Task<IEnumerable<T>> GetAllFriendRequestAsync<T>(string userId);
    }
}
