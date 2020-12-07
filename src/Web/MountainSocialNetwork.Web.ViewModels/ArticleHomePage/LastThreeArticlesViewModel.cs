namespace MountainSocialNetwork.Web.ViewModels.BlogHomePage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class LastThreeArticlesViewModel : IMapFrom<Article>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CommentsCount { get; set; }

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

        public string ArticlePicturesPictureURL { get; set; }

        //public void CreateMappings(IProfileExpression configuration)
        //{
        //    configuration.CreateMap<Recipe, RecipeInListViewModel>()
        //        .ForMember(x => x.ImageUrl, opt =>
        //            opt.MapFrom(x =>
        //                x.Images.FirstOrDefault().RemoteImageUrl != null ?
        //                x.Images.FirstOrDefault().RemoteImageUrl :
        //                "/images/recipes/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        //}

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Article, LastThreeArticlesViewModel>()
                .ForMember(x => x.ArticlePicturesPictureURL, opt =>
                opt.MapFrom(x =>
                x.ArticlePictures.OrderByDescending(p => p.CreatedOn).FirstOrDefault().PictureURL
                ));
        }
    }
}
