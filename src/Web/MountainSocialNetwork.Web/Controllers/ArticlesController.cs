namespace MountainSocialNetwork.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MountainSocialNetwork.Data.Common.Repositories;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Data;
    using MountainSocialNetwork.Web.ViewModels.BlogHomePage;
    using MountainSocialNetwork.Web.ViewModels.BlogPosts;

    public class ArticlesController : Controller
    {
        private readonly IArticlePostsService blogPostsService;

        private readonly UserManager<ApplicationUser> userManager;

        private readonly ICategoriesService categoriesService;

        private readonly ICloudinaryService cloudinaryService;

        public ArticlesController(IArticlePostsService blogPostsService, UserManager<ApplicationUser> userManager, ICategoriesService categoriesService, ICloudinaryService cloudinaryService)
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
        public async Task<IActionResult> Create(ArticlePostCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var blogPostId = await this.blogPostsService.CreateAsync(input.Title, input.Content, user.Id, input.CategoryId);

            foreach (var image in input.Picture)
            {
                // string name = DateTime.UtcNow.ToString("G", CultureInfo.InvariantCulture);
                string pictureUrl = await this.cloudinaryService.UploadPictureAsync(image, image.FileName);

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
    }
}
