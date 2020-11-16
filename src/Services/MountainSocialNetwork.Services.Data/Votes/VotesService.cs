namespace MountainSocialNetwork.Services.Data.Votes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MountainSocialNetwork.Data.Common.Repositories;
    using MountainSocialNetwork.Data.Models;

    public class VotesService : IVotesService
    {
        private readonly IRepository<Vote> votesRepository;

        public VotesService(IRepository<Vote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public int GetDownVotes(int newsFeedPostId)
        {
            var votes = this.votesRepository.All().Where(v => v.NewsFeedPostId == newsFeedPostId && v.IsUpVote == false).Count();

            return votes;
        }

        public int GetUpVotes(int newsFeedPostId)
        {
            var votes = this.votesRepository.All().Where(v => v.NewsFeedPostId == newsFeedPostId && v.IsUpVote == true).Count();

            return votes;
        }

        public async Task VoteAsync(int newsFeedPostId, string userId, bool isUpVote)
        {
            var vote = await this.votesRepository.All().FirstOrDefaultAsync(x => x.NewsFeedPostId == newsFeedPostId && userId == x.UserId);

            if (vote != null)
            {
                if (isUpVote == true)
                {
                    vote.IsUpVote = true;
                }
                else if (isUpVote == false)
                {
                    vote.IsUpVote = false;
                }
            }
            else
            {
                vote = new Vote
                {
                    NewsFeedPostId = newsFeedPostId,
                    UserId = userId,
                    IsUpVote = isUpVote,
                };

                await this.votesRepository.AddAsync(vote);
            }

            await this.votesRepository.SaveChangesAsync();
        }
    }
}
