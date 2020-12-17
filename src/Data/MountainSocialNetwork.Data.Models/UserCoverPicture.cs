namespace MountainSocialNetwork.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using MountainSocialNetwork.Data.Common.Models;

    public class UserCoverPicture : BaseModel<int>
    {
        [Required]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public string PictureURL { get; set; }

    }
}
