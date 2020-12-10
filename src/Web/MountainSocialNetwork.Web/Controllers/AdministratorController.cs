﻿namespace MountainSocialNetwork.Web.Controllers
{
    using System.Threading.Tasks;

    using Ganss.XSS;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Data.Administrator;
    using MountainSocialNetwork.Web.ViewModels;
    using MountainSocialNetwork.Web.ViewModels.Administration;
    using MountainSocialNetwork.Web.ViewModels.Comments;
    using MountainSocialNetwork.Web.ViewModels.NewsFeed;
    using MountainSocialNetwork.Web.ViewModels.UsersPosts;

    [Authorize(Roles = "Administrator")]
    public class AdministratorController : Controller
    {
        private readonly IAdministratorService administratorService;

        public AdministratorController(IAdministratorService administratorService)
        {
            this.administratorService = administratorService;
        }

        public IActionResult Administration()
        {
            return this.View();
        }

        [HttpGet]
        public async Task<IActionResult> NewsFeedPost()
        {
            var newsFeedPost = await this.administratorService.GetAllNewsFeedPost<NewsFeedPostAdministrationViewModel>();

            var model = new NewsFeedPostAdministrationResponseModel
            {
                NewsFeedPosts = newsFeedPost,
            };

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Articles()
        {
            var articles = await this.administratorService.GetAllArticlesPost<ArticleAdministrationViewModel>();

            var model = new ArticleAdministrationResponseModel
            {
                Articles = articles,
            };

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> NewsFeedComments()
        {
            var comments = await this.administratorService.GetAllNewsFeedComment<NewsFeedCommentViewModel>();

            var model = new NewsFeedCommentResponseModel
            {
                Comments = comments,
            };

            return this.View(model);
        }

        public async Task<IActionResult> DeleteNewsFeedPost(int id)
        {
            var post = await this.administratorService.GetNewsFeedPost(id);

            await this.administratorService.DeleteNewsFeedPost(post);

            return this.RedirectToAction(nameof(this.NewsFeedPost));
        }

        public async Task<IActionResult> DeleteNewsFeedComment(int id)
        {
            var post = await this.administratorService.GetNewsFeedComment(id);

            await this.administratorService.DeleteNewsFeedComment(post);

            return this.RedirectToAction(nameof(this.NewsFeedComments));
        }

        public async Task<IActionResult> DeleteArticle(int id)
        {
            var article = await this.administratorService.GetArticle(id);

            await this.administratorService.DeleteArticle(article);

            return this.RedirectToAction(nameof(this.Articles));
        }

        [HttpGet]
        public async Task<IActionResult> EditNewsFeedPost(int id)
        {
            var editViewModel = await this.administratorService.GetByIdNewsFeedPost<EditNewsFeedPostViewModel>(id);

            return this.View(editViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditNewsFeedPost(EditPostInputModel model)
        {
            var sanitizer = new HtmlSanitizer();

            var content = sanitizer.Sanitize(model.Content);

            var post = new NewsFeedPost
            {
                Id = model.Id,
                Content = content,
            };

            await this.administratorService.UpdateNewsFeedPost(post);

            return this.RedirectToAction(nameof(this.NewsFeedPost));
        }

        [HttpGet]
        public async Task<IActionResult> EditArticle(int id)
        {
            var editViewModel = await this.administratorService.GetByIdArticle<EditArticleInputModel>(id);

            return this.View(editViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditArticle(EditArticleInputModel model)
        {

            var sanitizer = new HtmlSanitizer();

            var content = sanitizer.Sanitize(model.Content);

            var newUpdatedPost = new Article
            {
                Id = model.Id,
                Title = model.Title,
                Content = content,
            };

            await this.administratorService.UpdateArticle(newUpdatedPost);

            return this.RedirectToAction(nameof(this.Articles));
        }

        public IActionResult DetailArticle(int id)
        {
            return this.RedirectToAction("ById", "Articles", new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> EditNewsFeedComment(int id)
        {
            var commentView = await this.administratorService.GetByIdComment<EditCommentViewModel>(id);

            return this.View(commentView);
        }

        [HttpPost]
        public async Task<IActionResult> EditNewsFeedComment(EditCommentViewModel model)
        {

            var sanitizer = new HtmlSanitizer();

            var content = sanitizer.Sanitize(model.Content);

            var comment = new NewsFeedComment
            {
                Id = model.Id,
                Content = model.Content,
            };

            await this.administratorService.UpdateComment(comment);

            return this.RedirectToAction(nameof(this.NewsFeedComments));
        }

        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            var categories = await this.administratorService.GetAllCategories<CategoryViewModel>();

            var model = new CategoriesResponseModel
            {
                Categories = categories,
            };

            return this.View(model);
        }

        [HttpGet]

        public IActionResult CreateCategory()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var category = new Category
            {
                Name = model.Name,
                Title = model.Name,
                Description = model.Name,
            };

            await this.administratorService.CreateCategory(category);

            return this.RedirectToAction(nameof(this.Categories));
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            await this.administratorService.DeleteCategory(id);

            return this.RedirectToAction(nameof(this.Categories));

        }

        public async Task<IActionResult> ArticleComments()
        {
            var comments = await this.administratorService.GetAllArticlesComment<ArticleCommentAdministrationViewModel>();

            var model = new ArticleCommentResponseModel
            {
                ArticleComments = comments,
            };

            return this.View(model);
        }

        public async Task<IActionResult> DeleteArticleComment(int id)
        {
            await this.administratorService.DeleteArticleComment(id);

            return this.RedirectToAction(nameof(this.ArticleComments));

        }
    }
}
