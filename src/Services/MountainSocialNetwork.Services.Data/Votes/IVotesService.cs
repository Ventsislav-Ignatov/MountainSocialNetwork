namespace MountainSocialNetwork.Services.Data.Votes
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IVotesService
    {
        Task VoteAsync(int newsFeedPostId, string userId, bool isUpVote);

        int GetUpVotes(int newsFeedPostId);

        int GetDownVotes(int newsFeedPostId);
    }
}
