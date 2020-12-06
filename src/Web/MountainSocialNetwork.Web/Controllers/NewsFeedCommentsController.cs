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
    using MountainSocialNetwork.Services.Data.NewsFeed;
    using MountainSocialNetwork.Web.ViewModels.Comments;
    using MountainSocialNetwork.Web.ViewModels.NewsFeed;
    using MountainSocialNetwork.Web.ViewModels.SocialTimeLine;

    public class NewsFeedCommentsController : Controller
    {
        private readonly INewsFeedCommentService newsFeedCommentService;
        private readonly UserManager<ApplicationUser> userManager;

        public NewsFeedCommentsController(INewsFeedCommentService newsFeedCommentService, UserManager<ApplicationUser> userManager)
        {
            this.newsFeedCommentService = newsFeedCommentService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.NoContent();
            }

            var userId = this.userManager.GetUserId(this.User);

            await this.newsFeedCommentService.Create(model.PostId, userId, model.Content, null);

            return this.RedirectToAction("NewsFeedContent", "NewsFeed");
        }
    }
}
