namespace MountainSocialNetwork.Web.ViewModels.NewsFeed
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class DeletePostInputModel : IMapFrom<NewsFeedPost>
    {
        [Required]
        public int Id { get; set; }

    }
}
