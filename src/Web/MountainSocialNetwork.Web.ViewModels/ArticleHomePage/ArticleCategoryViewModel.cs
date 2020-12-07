namespace MountainSocialNetwork.Web.ViewModels.BlogHomePage
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;

    using AutoMapper;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class ArticleCategoryViewModel : IMapFrom<Article>, IHaveCustomMappings
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

        public string PictureURL { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Article, ArticleCategoryViewModel>()
                .ForMember(x => x.PictureURL, opt =>
                opt.MapFrom(x =>
                x.ArticlePictures.OrderByDescending(p => p.CreatedOn).FirstOrDefault().PictureURL
                ));
        }
    }
}