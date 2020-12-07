namespace MountainSocialNetwork.Services.Data.NewsFeed
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using System.Linq;

    using MountainSocialNetwork.Data.Common.Repositories;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Web.ViewModels.NewsFeed;
    using Microsoft.EntityFrameworkCore;

    public class NewsFeedCommentService : INewsFeedCommentService
    {
        private readonly IDeletableEntityRepository<NewsFeedComment> commentsRepository;

        public NewsFeedCommentService(IDeletableEntityRepository<NewsFeedComment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public async Task Create(int newsFeedPostId, string userId, string content, int? parentId = null)
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

        public bool IsInPostId(int commentId, int articleId)
        {
            return true;
        }
    }
}
