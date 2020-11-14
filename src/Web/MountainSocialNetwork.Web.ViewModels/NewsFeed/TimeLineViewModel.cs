namespace MountainSocialNetwork.Web.ViewModels.SocialTimeLine
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class TimeLineViewModel : IMapFrom<NewsFeedPost>
    {
        public IEnumerable<TimeLineAllPostsViewModel> AllPosts { get; set; }

        public string Content { get; set; }

    }
}
