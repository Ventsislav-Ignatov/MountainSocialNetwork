namespace MountainSocialNetwork.Web.ViewModels.NewsFeed
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class EditPostInputModel : IMapFrom<NewsFeedPost>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(250)]
        public string Content { get; set; }
    }
}
