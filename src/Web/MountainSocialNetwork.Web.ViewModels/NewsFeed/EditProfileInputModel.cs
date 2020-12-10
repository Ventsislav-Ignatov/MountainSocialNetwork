namespace MountainSocialNetwork.Web.ViewModels.NewsFeed
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class EditProfileInputModel : IMapFrom<ApplicationUser>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Town { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }

        public string Description { get; set; }

        public string CreatedOn { get; set; }

        public IFormFile ProfilePicture { get; set; }

        public IFormFile CoverPhoto { get; set; }

        public string PictureURL { get; set; }
    }
}
