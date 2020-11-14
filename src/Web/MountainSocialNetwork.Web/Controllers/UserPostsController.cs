namespace MountainSocialNetwork.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Data;
    using MountainSocialNetwork.Web.ViewModels.BlogPosts;
    using MountainSocialNetwork.Web.ViewModels.UsersPosts;

    public class UserPostsController : Controller
    {
        private readonly IArticleByUserService blogPostsByUser;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IArticlePostsService postsService;
        private readonly IFavouritePostService favouritePostService;

        public UserPostsController(IArticleByUserService blogPostsByUser, UserManager<ApplicationUser> userManager, IArticlePostsService postsService, IFavouritePostService favouritePostService)
        {
            this.blogPostsByUser = blogPostsByUser;
            this.userManager = userManager;
            this.postsService = postsService;
            this.favouritePostService = favouritePostService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ArticlePostByUser()
        {
            var model = new UsersPostsByIdViewModel();

            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = await this.blogPostsByUser.GetAll<UserPostByIdModel>(user.Id);

            model.Posts = viewModel;

            return this.View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (!await this.blogPostsByUser.Exists(id, user.Id))
            {
                return this.RedirectToAction("NotOwner", "NewsFeed");
            }

            var editViewModel = await this.postsService.GetById<EditPostInputModel>(id);

            return this.View(editViewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(EditPostInputModel editPostInputModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (!await this.blogPostsByUser.Exists(editPostInputModel.Id, user.Id))
            {
                return this.RedirectToAction("NotOwner", "NewsFeed");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(editPostInputModel);
            }

            var newUpdatedPost = new Article
            {
                Id = editPostInputModel.Id,
                Title = editPostInputModel.Title,
                Content = editPostInputModel.Content,
            };

            await this.blogPostsByUser.Update(newUpdatedPost);

            return this.RedirectToAction(nameof(this.ArticlePostByUser));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllFavouriteArticles()
        {
            var viewModel = new UsersPostsByIdViewModel();

            var user = await this.userManager.GetUserAsync(this.User);

            var favouritePosts = await this.favouritePostService.GetAllFavouritePost<UserFavouriteArticlesViewModel>(user.Id);

            viewModel.FavouritePosts = favouritePosts;

            return this.View(viewModel);
        }
    }
}
