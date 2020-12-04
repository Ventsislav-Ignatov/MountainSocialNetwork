namespace MountainSocialNetwork.Web.ViewModels.Gallery
{
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class ProfileGalleryViewModel : IMapFrom<UserProfilePicture>
    {
        public string PictureURL { get; set; }
    }
}
