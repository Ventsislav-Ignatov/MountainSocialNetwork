namespace MountainSocialNetwork.Web.ViewModels.NewsFeed
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class EditNewsFeedPostInputModel : IMapFrom<NewsFeedPost>
    {

        public string Content { get; set; }
    }
}
