namespace MountainSocialNetwork.Web.ViewModels.Friend
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UserRequestFriendshipResponseModel
    {
        public IEnumerable<UserRequestFriendshipViewModel> Friendship { get; set; }
    }
}
