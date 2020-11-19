namespace MountainSocialNetwork.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;

    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class HomeBlogArticleViewModel : IMapFrom<Article>
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ShortContent
        {
            get
            {
                return this.Content.Length > 200 ? this.Content.Substring(0, 200) + "..." : this.Content;
            }
        }

        public string UserUserName { get; set; }

        public int CommentsCount { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
