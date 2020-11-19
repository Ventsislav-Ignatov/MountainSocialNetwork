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
        [MinLength(5)]
        [MaxLength(250)]
        public string Content { get; set; }

    }
}
