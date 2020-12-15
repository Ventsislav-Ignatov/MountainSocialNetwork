namespace MountainSocialNetwork.Web.ViewModels.Friend
{
    using AutoMapper;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class UserFriendshipViewModel
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PictureURL { get; set; }
    }
}
