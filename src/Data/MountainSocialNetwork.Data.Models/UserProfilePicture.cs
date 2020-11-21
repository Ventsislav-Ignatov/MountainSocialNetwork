namespace MountainSocialNetwork.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MountainSocialNetwork.Data.Common.Models;

    public class UserProfilePicture : BaseModel<int>
    {
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string PictureURL { get; set; }
    }
}
