namespace MountainSocialNetwork.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Data;
    using MountainSocialNetwork.Services.Data.TimeLine;
    using MountainSocialNetwork.Web.ViewModels.NewsFeed;
    using MountainSocialNetwork.Web.ViewModels.SocialTimeLine;

    public class NewsFeedController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly INewsFeedService newsFeedService;

        public NewsFeedController(UserManager<ApplicationUser> userManager, INewsFeedService newsFeedService)
        {
            this.userManager = userManager;
            this.newsFeedService = newsFeedService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult NewsFeedContent()
        {
            //var posts = new TimeLineViewModel();

            //var allPosts = this.newsFeedService.GetAllSocialPosts<TimeLineAllPostsViewModel>();

            //posts.AllPosts = allPosts;
            //return this.View(posts);

            var posts = new TimeLineViewModel();

            var allPost = this.newsFeedService.GetAllSocialPosts();

            posts.AllPosts = allPost;

            return this.View(posts);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePost(TimeLineViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.newsFeedService.CreateAsync(model.Content, user.Id);

            return this.Redirect(nameof(this.NewsFeedContent));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditNewsFeedPost(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (!await this.newsFeedService.ExistsAndOwner(id, user.Id))
            {
                return this.RedirectToAction(nameof(this.NotOwner));
            }

            var editViewModel = await this.newsFeedService.GetById<EditNewsFeedPostInputModel>(id);

            return this.View(editViewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditNewsFeedPost(EditPostInputModel model)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (!await this.newsFeedService.ExistsAndOwner(model.Id, user.Id))
            {
                return this.RedirectToAction(nameof(this.NotOwner));
            }

            var post = new NewsFeedPost
            {
                Id = model.Id,
                Content = model.Content,
            };

            await this.newsFeedService.Update(post);

            return this.RedirectToAction(nameof(this.NewsFeedContent));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(DeletePostInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.NotOwner));
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var hasRight = await this.newsFeedService.ExistsAndOwner(model.Id, user.Id);

            if (hasRight)
            {
                var post = await this.newsFeedService.GetNewsFeedPost(model.Id);

                await this.newsFeedService.Delete(post);
            }

            return this.RedirectToAction(nameof(this.NewsFeedContent));
        }

        public IActionResult NotOwner()
        {
            return this.View();
        }

        public IActionResult EditProfile()
        {
            return this.View();
        }
    }
}
