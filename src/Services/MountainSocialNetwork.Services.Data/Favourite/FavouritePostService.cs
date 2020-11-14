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
    using MountainSocialNetwork.Services.Mapping;

    public class FavouritePostService : IFavouritePostService
    {
        private readonly IDeletableEntityRepository<UserFavouriteArticle> repository;

        public FavouritePostService(IDeletableEntityRepository<UserFavouriteArticle> repository)
        {
            this.repository = repository;
        }

        public async Task AddFavouritePost(int articleId, string userId)
        {
            var post = new UserFavouriteArticle
            {
                ArticleId = articleId,
                UserId = userId,
            };

            await this.repository.AddAsync(post);
            await this.repository.SaveChangesAsync();
        }

        public async Task<bool> AlreadyAdded(int articleId, string userId)
        {
            var post = await this.repository.All().Where(a => a.ArticleId == articleId && a.UserId == userId).FirstOrDefaultAsync();

            if (post != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<T>> GetAllFavouritePost<T>(string userId)
        {
            return await this.repository
                    .All()
                    .Where(x => x.UserId == userId)
                    .To<T>()
                    .ToListAsync();
        }
    }
}
