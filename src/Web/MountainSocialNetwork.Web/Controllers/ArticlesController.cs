namespace MountainSocialNetwork.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Ganss.XSS;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MountainSocialNetwork.Data.Common.Repositories;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Data;
    using MountainSocialNetwork.Web.ViewModels.BlogHomePage;
    using MountainSocialNetwork.Web.ViewModels.BlogPosts;
    using MountainSocialNetwork.Web.ViewModels.UsersPosts;

    public class ArticlesController : Controller
    {
        private const string FolderName = "ArticlePostsPictures";

        private readonly IArticlePostService blogPostsService;

        private readonly UserManager<ApplicationUser> userManager;

        private readonly ICategoriesService categoriesService;

        private readonly ICloudinaryService cloudinaryService;

        public ArticlesController(IArticlePostService blogPostsService, UserManager<ApplicationUser> userManager, ICategoriesService categoriesService, ICloudinaryService cloudinaryService)
        {
            this.blogPostsService = blogPostsService;
            this.userManager = userManager;
            this.categoriesService = categoriesService;
            this.cloudinaryService = cloudinaryService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await this.categoriesService.GetAll<CategoryDropDownViewModel>();

            var viewModel = new ArticlePostCreateInputModel()
            {
                Categories = categories,
            };

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(ArticlePostCreateInputModel model)
        {

            if (!await this.categoriesService.CategoryExits(model.CategoryId))
            {
                return this.RedirectToAction(nameof(this.Create));
            }

            if (!this.ModelState.IsValid)
            {
                var categories = await this.categoriesService.GetAll<CategoryDropDownViewModel>();
                model.Categories = categories;
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var sanitizer = new HtmlSanitizer();

            var content = sanitizer.Sanitize(model.Content);

            var blogPostId = await this.blogPostsService.CreateAsync(model.Title, content, user.Id, model.CategoryId);

            foreach (var image in model.Picture)
            {
                // string name = DateTime.UtcNow.ToString("G", CultureInfo.InvariantCulture);
                string pictureUrl = await this.cloudinaryService.UploadPictureAsync(image, image.FileName, FolderName);

                await this.blogPostsService.CreateArticlePicturesAsync(blogPostId, user.Id, pictureUrl);
            }

            return this.RedirectToAction(nameof(this.ById), new { id = blogPostId });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ById(int id)
        {
            var postViewModel = await this.blogPostsService.GetById<ArticleByIdViewModel>(id);

            return this.View(postViewModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (!await this.blogPostsService.Exists(id, user.Id))
            {
                return this.RedirectToAction("NotOwner", "NewsFeed");
            }

            var editViewModel = await this.blogPostsService.GetById<EditArticleInputModel>(id);

            return this.View(editViewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(EditArticleInputModel model)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (!await this.blogPostsService.Exists(model.Id, user.Id))
            {
                return this.RedirectToAction("NotOwner", "NewsFeed");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var sanitizer = new HtmlSanitizer();

            var content = sanitizer.Sanitize(model.Content);

            var newUpdatedPost = new Article
            {
                Id = model.Id,
                Title = model.Title,
                Content = content,
            };

            await this.blogPostsService.Update(newUpdatedPost);

            return this.RedirectToAction("ById", "Articles", new { id = model.Id });

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ArticlePostByUser()
        {
            var model = new UsersPostsByIdViewModel();

            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = await this.blogPostsService.GetAll<UserPostByIdModel>(user.Id);

            model.Posts = viewModel;

            return this.View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllFavouriteArticles()
        {
            var viewModel = new UsersPostsByIdViewModel();

            var user = await this.userManager.GetUserAsync(this.User);

            var favouritePosts = await this.blogPostsService.GetAllFavouritePost<UserFavouriteArticlesViewModel>(user.Id);

            viewModel.FavouritePosts = favouritePosts;

            return this.View(viewModel);
        }
    }
}
