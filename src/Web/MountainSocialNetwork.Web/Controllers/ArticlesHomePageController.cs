namespace MountainSocialNetwork.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MountainSocialNetwork.Data;
    using MountainSocialNetwork.Data.Common.Repositories;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Data;
    using MountainSocialNetwork.Web.ViewModels;
    using MountainSocialNetwork.Web.ViewModels.BlogHomePage;
    using MountainSocialNetwork.Web.ViewModels.FavouritePosts;

    public class ArticlesHomePageController : Controller
    {
        private const int PostPerPage = 3;

        private readonly ICategoriesService categories;
        private readonly IArticleHomePageService articles;
        private readonly IFavouritePostService favouritePostService;
        private readonly UserManager<ApplicationUser> userManager;

        public ArticlesHomePageController(ICategoriesService categories, IArticleHomePageService articles, IFavouritePostService favouritePostService, UserManager<ApplicationUser> userManager)
        {
            this.categories = categories;
            this.articles = articles;
            this.favouritePostService = favouritePostService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> HomePage(int id = 1)
        {
            var viewModel = new HomeViewModel
            {
                PostsPerPage = PostPerPage,
                PageNumber = id,
                PostsCount = this.articles.GetPostsCount(),
                Categories = await this.categories.GetAll<HomeCategoryViewModel>(),
                Posts = this.articles.GetAllArticlePosts<HomeBlogArticleViewModel>(id, PostPerPage),
                LastThreeArticles = await this.articles.LastThreePosts<LastThreeArticlesViewModel>(),
            };
            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> CategoriesByName(string name)
        {
            var categoriesByName = await this.categories.CategoriesByName<CategoryByNameViewModel>(name);

            return this.View(categoriesByName);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateFavouriteArticle(FavouritePostInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var isAlreadyAdd = await this.favouritePostService.AlreadyAdded(model.PostId, user.Id);

            if (!isAlreadyAdd)
            {
                await this.favouritePostService.AddFavouritePost(model.PostId, user.Id);
            }
            else
            {
                this.ModelState.AddModelError(model.PostId.ToString(), "Already added!");
            }

            return this.RedirectToAction("GetAllFavouriteArticles", "UserPosts");
        }
    }
}
