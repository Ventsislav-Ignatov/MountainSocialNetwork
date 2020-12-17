namespace MountainSocialNetwork.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Data.Friend;
    using MountainSocialNetwork.Web.ViewModels.Friend;

    public class FriendController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IFriendService friendService;

        public FriendController(UserManager<ApplicationUser> userManager, IFriendService friendService)
        {
            this.userManager = userManager;
            this.friendService = friendService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetFriendRequest()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var model = new UserRequestFriendshipResponseModel { };

            var friends = await this.friendService.GetAllFriendRequestAsync<UserRequestFriendshipViewModel>(user.Id);

            model.Friendship = friends;

            return this.View(model);

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SendFriendRequest(string userName)
        {
            var userSender = await this.userManager.GetUserAsync(this.User);

            var userReceiver = await this.userManager.FindByNameAsync(userName);

            var alredyFriend = await this.friendService.AlredyFriendOrSendFriendRequestAsync(userSender.Id, userReceiver.Id);

            if (alredyFriend)
            {
                return this.View(nameof(this.AlreadyFriend));
            }

            await this.friendService.CreateFriendRequestAsync(userSender.Id, userReceiver.Id);

            return this.RedirectToAction("NewsFeedContent", "NewsFeed");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ApproveFriendRequest(string receiverId, string senderName)
        {
            var userReceiver = await this.userManager.FindByNameAsync(receiverId);

            var userSender = await this.userManager.FindByNameAsync(senderName);

            await this.friendService.ApproveFriendRequestAsync(userSender.Id, userReceiver.Id);

            return this.RedirectToAction("NewsFeedContent", "NewsFeed");

        }

        [Authorize]
        [HttpPost]

        public async Task<IActionResult> DeclineFriendRequest(string receiverId, string senderName)
        {
            var userReceiver = await this.userManager.FindByNameAsync(receiverId);

            var userSender = await this.userManager.FindByNameAsync(senderName);

            await this.friendService.DeclineFriendRequestAsync(userSender.Id, userReceiver.Id);

            return this.RedirectToAction(nameof(this.GetFriendRequest));

        }

        [Authorize]
        [HttpGet]

        public IActionResult GetAllFriends(string userId)
        {
            var friends = this.friendService.GetAllFriendAsync(userId);

            var model = new UserFriendshipResponseModel
            {
                Friends = friends,
            };

            return this.View(model);
        }

        [Authorize]
        [HttpPost]

        public async Task<IActionResult> DeleteFriendShip(string userName)
        {
            var loggedUser = await this.userManager.GetUserAsync(this.User);

            var user = await this.userManager.FindByNameAsync(userName);

            await this.friendService.DeleteFriendShipAsync(loggedUser.Id, user.Id);

            return this.RedirectToAction("PersonalNewsFeedByUser", "NewsFeed", new { userName = user.UserName });
        }

        [Authorize]
        [HttpGet]
        public IActionResult AlreadyFriend()
        {
            return this.View();
        }

    }
}
