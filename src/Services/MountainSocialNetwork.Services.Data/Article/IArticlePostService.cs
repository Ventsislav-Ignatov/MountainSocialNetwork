namespace MountainSocialNetwork.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MountainSocialNetwork.Data.Models;

    public interface IArticlePostService
    {
        Task<int> CreateAsync(string title, string content, string userId, int categoryId);

        Task CreateArticlePicturesAsync(int articleId, string userId, string pictureURL);

        Task<T> GetById<T>(int id);

        Task<IEnumerable<T>> GetAll<T>(string userId);

        Task<Article> Update(Article article);

        Task<bool> Exists(int id, string authorId);

        Task AddFavouritePost(int articleId, string userId);

        Task<bool> AlreadyAdded(int articleId, string userId);

        Task<IEnumerable<T>> GetAllFavouritePost<T>(string userId);

    }
}
