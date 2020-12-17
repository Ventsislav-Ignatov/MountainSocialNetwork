namespace MountainSocialNetwork.Services.Data.NewsFeed
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using MountainSocialNetwork.Data.Common.Repositories;
    using MountainSocialNetwork.Data.Models;

    public class NewsFeedCommentService : INewsFeedCommentService
    {
        private readonly IDeletableEntityRepository<NewsFeedComment> commentsRepository;

        public NewsFeedCommentService(IDeletableEntityRepository<NewsFeedComment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public async Task CreateAsync(int newsFeedPostId, string userId, string content, int? parentId = null)
        {
            var comment = new NewsFeedComment
            {
                NewsFeedPostId = newsFeedPostId,
                UserId = userId,
                Content = content,
                ParentId = parentId,
                CreatedOn = DateTime.UtcNow,
            };

            await this.commentsRepository.AddAsync(comment);
            await this.commentsRepository.SaveChangesAsync();
        }

        public async Task DeleteWhenPostIsDeletedAsync(int postId)
        {
            var comments = await this.commentsRepository.AllAsNoTracking().Where(x => x.NewsFeedPostId == postId).ToListAsync();

            foreach (var comment in comments)
            {
                this.commentsRepository.Delete(comment);
            }

            await this.commentsRepository.SaveChangesAsync();
        }
    }
}
