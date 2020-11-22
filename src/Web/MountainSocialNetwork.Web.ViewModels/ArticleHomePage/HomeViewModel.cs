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

        public int PageNumber { get; set; }

        public int PostsCount { get; set; }

        public int PostsPerPage { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public int PreviousPageNumber => this.PageNumber - 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int NextPageNumber => this.PageNumber + 1;

        public int PagesCount => (int)Math.Ceiling((double)this.PostsCount / this.PostsPerPage);

    }
}
