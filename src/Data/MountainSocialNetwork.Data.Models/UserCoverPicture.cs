namespace MountainSocialNetwork.Data.Models
{
    using MountainSocialNetwork.Data.Common.Models;

    public class UserCoverPicture : BaseModel<int>
    {
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string PictureURL { get; set; }

    }
}
