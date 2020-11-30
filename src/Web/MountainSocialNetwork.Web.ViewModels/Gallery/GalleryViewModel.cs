namespace MountainSocialNetwork.Web.ViewModels.Gallery
{
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class GalleryViewModel : IMapFrom<UserProfilePicture>
    {
        public string PictureURL { get; set; }
    }
}
