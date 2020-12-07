namespace MountainSocialNetwork.Web.ViewModels.UsersPosts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;

    using AutoMapper;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class UserFavouriteArticlesViewModel : IMapFrom<UserFavouriteArticle>, IHaveCustomMappings
    {
        public int ArticleId { get; set; }

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

        public string PictureUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<UserFavouriteArticle, UserFavouriteArticlesViewModel>()
                .ForMember(x => x.PictureUrl, opt =>
                opt.MapFrom(x =>
                x.Article.ArticlePictures.OrderByDescending(p => p.CreatedOn).FirstOrDefault().PictureURL));
        }
    }
}
