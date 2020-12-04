namespace MountainSocialNetwork.Web.ViewModels.Gallery
{
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class CoverGalleryViewModel : IMapFrom<UserCoverPicture>
    {
        public string PictureURL { get; set; }
    }
}
