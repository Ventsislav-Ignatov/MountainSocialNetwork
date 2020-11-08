namespace MountainSocialNetwork.Web.ViewModels.BlogHomePage
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MountainSocialNetwork.Web.ViewModels.Search;

    public class HomeViewModel
    {
        public IEnumerable<HomeCategoryViewModel> Categories { get; set; }

        public IEnumerable<HomeBlogArticleViewModel> Posts { get; set; }

        public SearchInputModel SearchInputModel { get; set; }

        public IEnumerable<HomeBlogArticleViewModel> SearchViewModel { get; set; }

        public IEnumerable<LastThreeArticlesViewModel> LastThreeArticles { get; set; }

    }
}
