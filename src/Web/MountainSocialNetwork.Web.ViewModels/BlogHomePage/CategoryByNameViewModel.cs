namespace MountainSocialNetwork.Web.ViewModels.BlogHomePage
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class CategoryByNameViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public IEnumerable<ArticleCategoryViewModel> ArticlePosts { get; set; }
    }
}
