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
    using MountainSocialNetwork.Services.Data.TimeLine;
    using MountainSocialNetwork.Web.ViewModels.SocialTimeLine;

    public class SocialTimeLineController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITimeLineService timeLineService;

        public SocialTimeLineController(UserManager<ApplicationUser> userManager, ITimeLineService timeLineService)
        {
            this.userManager = userManager;
            this.timeLineService = timeLineService;
        }


        public IActionResult Timeline()
        {
            var posts = new TimeLineViewModel();

            var allPosts = this.timeLineService.GetAllSocialPosts<TimeLineAllPostsViewModel>();

            posts.AllPosts = allPosts;
            return this.View(posts);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePost(TimelineCreatePostInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.timeLineService.CreateAsync(model.Content, user.Id);

            return this.Redirect(nameof(this.Timeline));
        }
    }
}
