namespace MountainSocialNetwork.Web.Controllers
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
            var newsFeedPost = await this.administratorService.GetAllNewsFeedPostAsync<NewsFeedPostAdministrationViewModel>();

            var model = new NewsFeedPostAdministrationResponseModel
            {
                NewsFeedPosts = newsFeedPost,
            };

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Articles()
        {
            var articles = await this.administratorService.GetAllArticlesPostAsync<ArticleAdministrationViewModel>();

            var model = new ArticleAdministrationResponseModel
            {
                Articles = articles,
            };

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> NewsFeedComments()
        {
            var comments = await this.administratorService.GetAllNewsFeedCommentAsync<NewsFeedCommentViewModel>();

            var model = new NewsFeedCommentResponseModel
            {
                Comments = comments,
            };

            return this.View(model);
        }

        public async Task<IActionResult> DeleteNewsFeedPost(int id)
        {
            var post = await this.administratorService.GetNewsFeedPostAsync(id);

            await this.administratorService.DeleteNewsFeedPostAsync(post);

            return this.RedirectToAction(nameof(this.NewsFeedPost));
        }

        public async Task<IActionResult> DeleteNewsFeedComment(int id)
        {
            var post = await this.administratorService.GetNewsFeedCommentAsync(id);

            await this.administratorService.DeleteNewsFeedCommentAsync(post);

            return this.RedirectToAction(nameof(this.NewsFeedComments));
        }

        public async Task<IActionResult> DeleteArticle(int id)
        {
            var article = await this.administratorService.GetArticleAsync(id);

            await this.administratorService.DeleteArticleAsync(article);

            return this.RedirectToAction(nameof(this.Articles));
        }

        [HttpGet]
        public async Task<IActionResult> EditNewsFeedPost(int id)
        {
            var editViewModel = await this.administratorService.GetByIdNewsFeedPostAsync<EditNewsFeedPostViewModel>(id);

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

            await this.administratorService.UpdateNewsFeedPostAsync(post);

            return this.RedirectToAction(nameof(this.NewsFeedPost));
        }

        [HttpGet]
        public async Task<IActionResult> EditArticle(int id)
        {
            var editViewModel = await this.administratorService.GetByIdArticleAsync<EditArticleInputModel>(id);

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

            await this.administratorService.UpdateArticleAsync(newUpdatedPost);

            return this.RedirectToAction(nameof(this.Articles));
        }

        public IActionResult DetailArticle(int id)
        {
            return this.RedirectToAction("ById", "Articles", new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> EditNewsFeedComment(int id)
        {
            var commentView = await this.administratorService.GetByIdCommentAsync<EditCommentViewModel>(id);

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

            await this.administratorService.UpdateCommentAsync(comment);

            return this.RedirectToAction(nameof(this.NewsFeedComments));
        }

        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            var categories = await this.administratorService.GetAllCategoriesAsync<CategoryViewModel>();

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

            await this.administratorService.CreateCategoryAsync(model.Name);

            return this.RedirectToAction(nameof(this.Categories));
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            await this.administratorService.DeleteCategoryAsync(id);

            return this.RedirectToAction(nameof(this.Categories));

        }

        public async Task<IActionResult> ArticleComments()
        {
            var comments = await this.administratorService.GetAllArticlesCommentAsync<ArticleCommentAdministrationViewModel>();

            var model = new ArticleCommentResponseModel
            {
                ArticleComments = comments,
            };

            return this.View(model);
        }

        public async Task<IActionResult> DeleteArticleComment(int id)
        {
            await this.administratorService.DeleteArticleCommentAsync(id);

            return this.RedirectToAction(nameof(this.ArticleComments));

        }
    }
}
