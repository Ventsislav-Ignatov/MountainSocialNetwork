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
                var content = WebUtility.HtmlDecode(Regex.Replace(this.ArticleContent, @"<[^>]+>", string.Empty));
                return content.Length > 200 ? content.Substring(0, 200) + "..." : content;
            }
        }

        public string ArticleUserUserName { get; set; }

        public int CommentsCount { get; set; }

        public DateTime ArticleCreatedOn { get; set; }
    }
}
