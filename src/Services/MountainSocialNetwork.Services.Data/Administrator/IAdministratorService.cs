namespace MountainSocialNetwork.Services.Data.Administrator
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MountainSocialNetwork.Data.Models;

    public interface IAdministratorService
    {
         Task<IEnumerable<T>> GetAllNewsFeedPost<T>();

         Task<IEnumerable<T>> GetAllArticlesPost<T>();

         Task<IEnumerable<T>> GetAllNewsFeedComment<T>();

         Task<IEnumerable<T>> GetAllArticlesComment<T>();

         Task<IEnumerable<T>> GetAllCategories<T>();

         Task DeleteNewsFeedPost(NewsFeedPost newsFeedPost);

         Task DeleteArticle(Article article);

         Task DeleteNewsFeedComment(NewsFeedComment comment);

         Task<Article> GetArticle(int id);

         Task<NewsFeedComment> GetNewsFeedComment(int id);

         Task<NewsFeedPost> UpdateNewsFeedPost(NewsFeedPost article);

         Task<Article> UpdateArticle(Article article);

         Task<NewsFeedComment> UpdateComment(NewsFeedComment comment);

         Task<T> GetByIdNewsFeedPost<T>(int id);

         Task<T> GetByIdArticle<T>(int id);

         Task<T> GetByIdComment<T>(int id);

         Task<NewsFeedPost> GetNewsFeedPost(int id);

         Task CreateCategory(Category category);

         Task DeleteCategory(int id);

         Task DeleteArticleComment(int id);

    }
}
