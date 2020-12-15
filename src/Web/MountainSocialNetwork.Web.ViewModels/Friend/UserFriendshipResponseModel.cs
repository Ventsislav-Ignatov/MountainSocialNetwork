namespace MountainSocialNetwork.Web.ViewModels.Friend
{
    using System.Collections.Generic;

    public class UserFriendshipResponseModel
    {
        public IEnumerable<UserFriendshipViewModel> Friends { get; set; }
    }
}
