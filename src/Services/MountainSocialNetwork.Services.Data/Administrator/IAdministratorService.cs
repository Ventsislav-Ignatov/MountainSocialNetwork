namespace MountainSocialNetwork.Services.Data.Administrator
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MountainSocialNetwork.Data.Models;

    public interface IAdministratorService
    {
         Task<IEnumerable<T>> GetAllNewsFeedPostAsync<T>();

         Task<IEnumerable<T>> GetAllArticlesPostAsync<T>();

         Task<IEnumerable<T>> GetAllNewsFeedCommentAsync<T>();

         Task<IEnumerable<T>> GetAllArticlesCommentAsync<T>();

         Task<IEnumerable<T>> GetAllCategoriesAsync<T>();

         Task DeleteNewsFeedPostAsync(NewsFeedPost newsFeedPost);

         Task DeleteArticleAsync(Article article);

         Task DeleteNewsFeedCommentAsync(NewsFeedComment comment);

         Task<Article> GetArticleAsync(int id);

         Task<NewsFeedComment> GetNewsFeedCommentAsync(int id);

         Task<NewsFeedPost> UpdateNewsFeedPostAsync(NewsFeedPost article);

         Task<Article> UpdateArticleAsync(Article article);

         Task<NewsFeedComment> UpdateCommentAsync(NewsFeedComment comment);

         Task<T> GetByIdNewsFeedPostAsync<T>(int id);

         Task<T> GetByIdArticleAsync<T>(int id);

         Task<T> GetByIdCommentAsync<T>(int id);

         Task<NewsFeedPost> GetNewsFeedPostAsync(int id);

         Task<int> CreateCategoryAsync(string name);

         Task DeleteCategoryAsync(int id);

         Task DeleteArticleCommentAsync(int id);

    }
}
