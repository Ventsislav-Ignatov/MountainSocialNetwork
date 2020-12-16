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
    using MountainSocialNetwork.Services.Data.Friend;
    using MountainSocialNetwork.Services.Data.NewsFeed;
    using MountainSocialNetwork.Services.Data.TimeLine;
    using MountainSocialNetwork.Web.ViewModels.Gallery;
    using MountainSocialNetwork.Web.ViewModels.NewsFeed;
    using MountainSocialNetwork.Web.ViewModels.SocialTimeLine;

    public class NewsFeedController : Controller
    {
        private const string FolderNameProfilePicture = "UserProfilePictures";
        private const string FolderNameCoverPicture = "UserCoverPictures";
        private const int PostPerPage = 4;

        private readonly UserManager<ApplicationUser> userManager;
        private readonly INewsFeedService newsFeedService;
        private readonly INewsFeedCommentService newsFeedCommentService;
        private readonly ICloudinaryService cloudinary;
        private readonly IFriendService friendService;

        public NewsFeedController(UserManager<ApplicationUser> userManager, INewsFeedService newsFeedService, INewsFeedCommentService newsFeedCommentService, 
            ICloudinaryService cloudinary, IFriendService friendService)
        {
            this.userManager = userManager;
            this.newsFeedService = newsFeedService;
            this.newsFeedCommentService = newsFeedCommentService;
            this.cloudinary = cloudinary;
            this.friendService = friendService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> NewsFeedContent(int id = 1)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var model = new TimeLineViewModel
            {
                PostsPerPage = PostPerPage,
                PageNumber = id,
                AllPosts = this.newsFeedService.GetAllSocialPosts<TimeLineAllPostsViewModel>(id, PostPerPage),
                PostsCount = this.newsFeedService.GetPostsCount(),
                NewsComments = await this.newsFeedService.GetAllComments(),
            };

            model.ProfilePictureUrl = await this.newsFeedService.LastProfilePicture(user.Id);
            model.CoverPictureUrl = await this.newsFeedService.LastCoverPicture(user.Id);

            return this.View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> PersonalNewsFeed(int id = 1)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var model = new TimeLineViewModel
            {
                PostsPerPage = PostPerPage,
                PageNumber = id,
                AllPosts = this.newsFeedService.GetAllSocialPostsByUser<TimeLineAllPostsViewModel>(user.Id, id, PostPerPage),
                PostsCount = this.newsFeedService.GetPostsCountByUser(user.Id),
                NewsComments = await this.newsFeedService.GetAllComments(),
                FriendCount = await this.newsFeedService.GetFriendCount(user.Id),
                RequestFriendCount = await this.friendService.RequestFriendCount(user.Id),
            };

            model.ProfilePictureUrl = await this.newsFeedService.LastProfilePicture(user.Id);
            model.CoverPictureUrl = await this.newsFeedService.LastCoverPicture(user.Id);

            return this.View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> PersonalNewsFeedByUser(string userName, int id = 1)
        {
            var loggedUser = await this.userManager.GetUserAsync(this.User);

            var user = await this.userManager.FindByNameAsync(userName);

            var model = new TimeLineViewModel
            {
                PostsPerPage = PostPerPage,
                PageNumber = id,
                UserName = user.UserName,
                AllPosts = this.newsFeedService.GetAllSocialPostsByUser<TimeLineAllPostsViewModel>(user.Id, id, PostPerPage),
                PostsCount = this.newsFeedService.GetPostsCountByUser(user.Id),
                NewsComments = await this.newsFeedService.GetAllComments(),
                IsFried = await this.friendService.AreTwoUsersFriends(loggedUser.Id, user.Id),
            };

            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Description = user.Description;
            model.Town = user.Town;
            model.BirthDay = user.BirthDay.ToString("d", CultureInfo.InvariantCulture);
            model.ProfilePictureUrl = await this.newsFeedService.LastProfilePicture(user.Id);
            model.CoverPictureUrl = await this.newsFeedService.LastCoverPicture(user.Id);

            return this.View(model);


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

                await this.newsFeedCommentService.DeleteWhenPostIsDeleted(post.Id);

                await this.newsFeedService.Delete(post);
            }
            else
            {
                return this.RedirectToAction(nameof(this.NotOwner));
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
                PictureURL = await this.newsFeedService.LastProfilePicture(user.Id),
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
            if (model.ProfilePicture != null)
            {
            string pictureUrlProfile = await this.cloudinary.UploadPictureAsync(model.ProfilePicture, model.ProfilePicture.FileName, FolderNameProfilePicture);
            await this.newsFeedService.CreateProfilePicture(user.Id, pictureUrlProfile);
            }

            if (model.CoverPhoto != null)
            {
            string pictureUrlCover = await this.cloudinary.UploadPictureAsync(model.CoverPhoto, model.CoverPhoto.FileName, FolderNameCoverPicture);
            await this.newsFeedService.CreateCoverPicture(user.Id, pictureUrlCover);
            }

            return this.RedirectToAction(nameof(this.NewsFeedContent));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ProfilePicturesGallery()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var pictures = await this.newsFeedService.GetAllProfilePictures<ProfileGalleryViewModel>(user.Id);

            var viewModel = new ProfileGalleryResponseViewModel
            {
                UserProfilePictures = pictures,
            };

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ProfilePicturesGalleryForUser(string userName)
        {
            var user = await this.userManager.FindByNameAsync(userName);

            var pictures = await this.newsFeedService.GetAllProfilePictures<ProfileGalleryViewModel>(user.Id);

            var viewModel = new ProfileGalleryResponseViewModel
            {
                UserProfilePictures = pictures,
            };

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CoverPicturesGallery()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var coverPictures = await this.newsFeedService.GetAllCoverPictures<CoverGalleryViewModel>(user.Id);

            var viewModel = new CoverGalleryResponseViewModel
            {
                UserCoverPictures = coverPictures,
            };

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CoverPicturesGalleryForUser(string userName)
        {
            var user = await this.userManager.FindByNameAsync(userName);

            var coverPictures = await this.newsFeedService.GetAllCoverPictures<CoverGalleryViewModel>(user.Id);

            var viewModel = new CoverGalleryResponseViewModel
            {
                UserCoverPictures = coverPictures,
            };

            return this.View(viewModel);
        }
    }
}
