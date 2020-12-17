namespace MountainSocialNetwork.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MountainSocialNetwork.Data.Models;

    public interface IArticlePostService
    {
        Task<int> CreateAsync(string title, string content, string userId, int categoryId);

        Task CreateArticlePicturesAsync(int articleId, string userId, string pictureURL);

        Task<T> GetByIdAsync<T>(int id);

        Task<IEnumerable<T>> GetAllAsync<T>(string userId);

        Task<Article> UpdateAsync(Article article);

        Task<bool> ExistsAsync(int id, string authorId);

        Task AddFavouritePostAsync(int articleId, string userId);

        Task<bool> AlreadyAddedAsync(int articleId, string userId);

        Task<IEnumerable<T>> GetAllFavouritePostAsync<T>(string userId);

    }
}
