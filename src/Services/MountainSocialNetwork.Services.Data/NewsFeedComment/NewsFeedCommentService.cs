namespace MountainSocialNetwork.Services.Data.NewsFeed
{
    using System;
    using System.Threading.Tasks;

    using MountainSocialNetwork.Data.Common.Repositories;
    using MountainSocialNetwork.Data.Models;

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
    }
}
