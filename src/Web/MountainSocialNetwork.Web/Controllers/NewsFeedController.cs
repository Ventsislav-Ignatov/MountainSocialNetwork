namespace MountainSocialNetwork.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Ganss.XSS;
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
        private const string FolderName = "UserProfilePictures";
        private const int PostPerPage = 4;

        private readonly UserManager<ApplicationUser> userManager;
        private readonly INewsFeedService newsFeedService;
        private readonly ICloudinaryService cloudinary;

        public NewsFeedController(UserManager<ApplicationUser> userManager, INewsFeedService newsFeedService , ICloudinaryService cloudinary)
        {
            this.userManager = userManager;
            this.newsFeedService = newsFeedService;
            this.cloudinary = cloudinary;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> NewsFeedContent(int id = 1)
        {
            //var posts = new TimeLineViewModel();

            //var allPosts = this.newsFeedService.GetAllSocialPosts<TimeLineAllPostsViewModel>();

            //posts.AllPosts = allPosts;
            //return this.View(posts);

            var user = await this.userManager.GetUserAsync(this.User);

            var posts = new TimeLineViewModel
            {
                PostsPerPage = PostPerPage,
                PageNumber = id,
                AllPosts = this.newsFeedService.GetAllSocialPosts(id, PostPerPage),
                PostsCount = this.newsFeedService.GetPostsCount(),
            };

            posts.FirstName = user.FirstName;
            posts.LastName = user.LastName;
            posts.Description = user.Description;
            posts.Town = user.Town;
            posts.BirthDay = user.BirthDay.ToString("d", CultureInfo.InvariantCulture);
            posts.PictureUrl = await this.newsFeedService.LastPicture(user.Id);

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

            var sanitizer = new HtmlSanitizer();

            var content = sanitizer.Sanitize(model.Content);

            await this.newsFeedService.CreateAsync(content, user.Id);

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

            var editViewModel = await this.newsFeedService.GetById<EditNewsFeedPostViewModel>(id);

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

            var sanitizer = new HtmlSanitizer();

            var content = sanitizer.Sanitize(model.Content);

            var post = new NewsFeedPost
            {
                Id = model.Id,
                Content = content,
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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var viewUser = new EditProfileInputModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDay = user.BirthDay,
                Description = user.Description,
                Town = user.Town,
                CreatedOn = user.CreatedOn.ToString(),
            };

            return this.View(viewUser);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditProfile(EditProfileInputModel model)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var newUser = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Description = model.Description,
                Town = model.Town,
                BirthDay = model.BirthDay,
            };

            await this.newsFeedService.EditProfile(newUser, user.Id);

            // string name = DateTime.UtcNow.ToString("G", CultureInfo.InvariantCulture);
            string pictureUrl = await this.cloudinary.UploadPictureAsync(model.ProfilePicture, model.ProfilePicture.FileName, FolderName);

            await this.newsFeedService.CreateProfilePicture(user.Id, pictureUrl);

            return this.RedirectToAction(nameof(this.NewsFeedContent));

        }
    }
}
