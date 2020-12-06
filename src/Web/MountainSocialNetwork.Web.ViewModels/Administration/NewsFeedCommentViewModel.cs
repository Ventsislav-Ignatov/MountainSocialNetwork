namespace MountainSocialNetwork.Web.ViewModels.Administration
{
    using System;

    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class NewsFeedCommentViewModel : IMapFrom<NewsFeedComment>
    {
        public int Id { get; set; }

        public int NewsFeedPostId { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public string UserUserName { get; set; }
    }
}
