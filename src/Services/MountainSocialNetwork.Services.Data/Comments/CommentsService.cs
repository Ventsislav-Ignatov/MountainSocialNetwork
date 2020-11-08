namespace MountainSocialNetwork.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MountainSocialNetwork.Data.Common.Repositories;
    using MountainSocialNetwork.Data.Models;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public CommentsService(IDeletableEntityRepository<Comment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public async Task Create(int articleId, string userId, string content, int? parentId = null)
        {
            var comment = new Comment
            {
                ArticleId = articleId,
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
            var commentPostId = this.commentsRepository.All().Where(x => x.Id == commentId)
               .Select(x => x.ArticleId).FirstOrDefault();
            return commentPostId == articleId;
        }

        public async Task<Comment> LastPublishedPost(string userId)
        {
            var comment = await this.commentsRepository
                .All()
                .Where(a => a.UserId == userId)
                .OrderByDescending(o => o.CreatedOn)
                .FirstOrDefaultAsync();

            return comment;
        }
    }
}
