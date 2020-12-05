namespace MountainSocialNetwork.Web.Controllers
{
    using System.Threading.Tasks;
    using Ganss.XSS;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Data.Administrator;
    using MountainSocialNetwork.Web.ViewModels.Administration;
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

        public async Task<IActionResult> NewsFeedPost()
        {
            var newsFeedPost = await this.administratorService.GetAllNewsFeedPost<NewsFeedPostAdministrationViewModel>();

            var model = new NewsFeedPostAdministrationResponseModel
            {
                NewsFeedPosts = newsFeedPost,
            };

            return this.View(model);
        }

        public async Task<IActionResult> Articles()
        {
            var articles = await this.administratorService.GetAllArticlesPost<ArticleAdministrationViewModel>();

            var model = new ArticleAdministrationResponseModel
            {
                Articles = articles,
            };

            return this.View(model);
        }

        public async Task<IActionResult> DeleteNewsFeedPost(int id)
        {
                var post = await this.administratorService.GetNewsFeedPost(id);

                await this.administratorService.DeleteNewsFeedPost(post);

                return this.RedirectToAction(nameof(this.NewsFeedPost));
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

    }
}
