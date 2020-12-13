namespace MountainSocialNetwork.Web.ViewModels
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
    using MountainSocialNetwork.Web.ViewModels.Pictures;

    public class HomeBlogArticleViewModel : IMapFrom<Article>, IHaveCustomMappings
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

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public int CommentsCount { get; set; }

        public DateTime CreatedOn { get; set; }

        public string PictureUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Article, HomeBlogArticleViewModel>()
                .ForMember(x => x.PictureUrl, opt =>
                opt.MapFrom(x =>
                x.ArticlePictures.OrderByDescending(p => p.CreatedOn).FirstOrDefault().PictureURL));
        }
    }
}
