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
    using MountainSocialNetwork.Web.ViewModels.Comments;

    public class CommentsController : Controller
    {
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(ICommentsService commentsService, UserManager<ApplicationUser> userManager)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentInputModel model)
        {
            var userId = this.userManager.GetUserId(this.User);

            var hasRightToPost = await this.commentsService.LastPublishedPost(userId);

            if (hasRightToPost != null)
            {
            int minutes = DateTime.UtcNow.Minute - hasRightToPost.CreatedOn.Minute;
            }

            //if (minutes < 1)
            //{
            //    return this.View(model);
            //}

            var parentId =
                 model.ParentId == 0 ?
                     (int?)null :
                     model.ParentId;

            if (parentId.HasValue)
            {
                if (!this.commentsService.IsInPostId(parentId.Value, model.PostId))
                {
                    return this.BadRequest();
                }
            }

            await this.commentsService.Create(model.PostId, userId, model.Content, parentId);
            return this.RedirectToAction("ById", "Articles", new { id = model.PostId });
        }
    }
}
