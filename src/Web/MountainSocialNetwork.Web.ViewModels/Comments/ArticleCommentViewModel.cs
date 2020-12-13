namespace MountainSocialNetwork.Web.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AutoMapper;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class ArticleCommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Content { get; set; }

        public string UserUserName { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public string OwnerPictureUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Comment, ArticleCommentViewModel>()
                .ForMember(x => x.OwnerPictureUrl, opt =>
                opt.MapFrom(x =>
                   x.User.UserProfilePictures.OrderByDescending(x => x.CreatedOn).FirstOrDefault().PictureURL));
        }
    }
}
