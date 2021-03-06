﻿namespace MountainSocialNetwork.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Ganss.XSS;
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

            var sanitizer = new HtmlSanitizer();

            var content = sanitizer.Sanitize(model.Content);

            await this.commentsService.CreateAsync(model.PostId, userId, content, parentId);
            return this.RedirectToAction("ById", "Articles", new { id = model.PostId });
        }
    }
}
