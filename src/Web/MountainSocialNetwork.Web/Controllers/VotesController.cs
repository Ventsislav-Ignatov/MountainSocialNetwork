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
    using MountainSocialNetwork.Services.Data.Votes;
    using MountainSocialNetwork.Web.ViewModels.NewsFeed;
    using MountainSocialNetwork.Web.ViewModels.SocialTimeLine;
    using MountainSocialNetwork.Web.ViewModels.Votes;

    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : ControllerBase
    {
        private readonly IVotesService voteService;
        private readonly UserManager<ApplicationUser> userManaganer;

        // POST /api/votes
        // Request body: {"NewsFeedPostId":1, "isUpVote":true}
        // Response body: {"votesCount: 13}
        public VotesController(IVotesService voteService, UserManager<ApplicationUser> userManaganer)
        {
            this.voteService = voteService;
            this.userManaganer = userManaganer;
        }

        [Authorize]
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<VoteResponseModel>> Post(VoteInputModel model)
        {
            var user = await this.userManaganer.GetUserAsync(this.User);

            await this.voteService.VoteAsync(model.NewsFeedPostId, user.Id, model.IsUpVote);

            var upVotes = this.voteService.GetUpVotes(model.NewsFeedPostId);
            var downVotes = this.voteService.GetDownVotes(model.NewsFeedPostId);

            return new VoteResponseModel { UpVotes = upVotes, DownVotes = downVotes };
        }
    }
}
