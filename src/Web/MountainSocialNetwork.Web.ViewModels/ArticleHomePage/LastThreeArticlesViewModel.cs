namespace MountainSocialNetwork.Web.ViewModels.BlogHomePage
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class LastThreeArticlesViewModel : IMapFrom<Article>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string UserUserName { get; set; }
    }
}
