namespace MountainSocialNetwork.Web.ViewModels.NewsFeed
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class EditPostInputModel : IMapFrom<NewsFeedPost>
    {
        public int Id { get; set; }

        public string Content { get; set; }
    }
}
