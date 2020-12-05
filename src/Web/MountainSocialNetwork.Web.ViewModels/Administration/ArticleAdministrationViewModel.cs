namespace MountainSocialNetwork.Web.ViewModels.Administration
{
    using System;

    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class ArticleAdministrationViewModel : IMapFrom<Article>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ShortContent
        {
            get
            {
                return this.Content.Length > 50 ? this.Content.Substring(0, 50) + "..." : this.Content;
            }
        }

        public DateTime CreatedOn { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public string UserUserName { get; set; }

        public string CategoryName { get; set; }
    }
}
