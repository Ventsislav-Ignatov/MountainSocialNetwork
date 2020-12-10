namespace MountainSocialNetwork.Web.ViewModels
{
    using System;

    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class ArticleCommentAdministrationViewModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public int ArticleId { get; set; }

        public int? ParentId { get; set; }

        public string Content { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
