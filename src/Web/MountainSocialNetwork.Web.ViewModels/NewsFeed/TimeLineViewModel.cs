namespace MountainSocialNetwork.Web.ViewModels.SocialTimeLine
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;
    using MountainSocialNetwork.Web.ViewModels.NewsFeed;

    public class TimeLineViewModel : IMapFrom<NewsFeedPost>
    {
        public IEnumerable<TimeLineAllPostsViewModel> AllPosts { get; set; }

        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Short content! Must be more than 5 symbols!")]
        [MaxLength(250, ErrorMessage = "Short content! Must be less than 250 symbols!")]
        public string Content { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Town { get; set; }

        public string BirthDay { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }

    }
}
