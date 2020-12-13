namespace MountainSocialNetwork.Web.ViewModels.Friend
{
    using System.Linq;

    using AutoMapper;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class UserRequestFriendshipViewModel : IMapFrom<FriendRequest>, IHaveCustomMappings
    {
        public string SenderUserName { get; set; }

        public string SenderFirstName { get; set; }

        public string SenderLastName { get; set; }

        public string PictureURL { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<FriendRequest, UserRequestFriendshipViewModel>()
                .ForMember(x => x.PictureURL, opt =>
                opt.MapFrom(x =>
                x.Sender.UserProfilePictures.OrderByDescending(p => p.CreatedOn).FirstOrDefault().PictureURL));
        }
    }
}
