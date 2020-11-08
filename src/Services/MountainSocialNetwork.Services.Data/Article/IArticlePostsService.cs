namespace MountainSocialNetwork.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IArticlePostsService
    {
        Task<int> CreateAsync(string title, string content, string userId, int categoryId);

        Task CreateArticlePicturesAsync(int articleId, string userId, string pictureURL);

        Task<T> GetById<T>(int id);
    }
}
