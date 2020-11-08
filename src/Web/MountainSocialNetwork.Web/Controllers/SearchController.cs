namespace MountainSocialNetwork.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MountainSocialNetwork.Services.Data.Search;
    using MountainSocialNetwork.Web.ViewModels;
    using MountainSocialNetwork.Web.ViewModels.BlogHomePage;
    using MountainSocialNetwork.Web.ViewModels.Search;

    public class SearchController : Controller
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        public async Task<IActionResult> SearchBar(SearchInputModel model)
        {
            var viewModel = new HomeViewModel { };

            var searchResult = await this.searchService.GetSearchedArticles<HomeBlogArticleViewModel>(model.Title);

            viewModel.SearchViewModel = searchResult;
            return this.View(viewModel);
        }
    }
}
