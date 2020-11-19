namespace MountainSocialNetwork.Web.ViewModels.UsersPosts
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;

    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class UserFavouriteArticlesViewModel : IMapFrom<UserFavouriteArticle>
    {
        public int Id { get; set; }

        public string ArticleTitle { get; set; }

        public string ArticleContent { get; set; }

        public string ShortContent
        {
            get
            {
                return this.ArticleContent.Length > 200 ? this.ArticleContent.Substring(0, 200) + "..." : this.ArticleContent;
            }
        }

        public string ArticleUserUserName { get; set; }

        public int CommentsCount { get; set; }

        public DateTime ArticleCreatedOn { get; set; }
    }
}
