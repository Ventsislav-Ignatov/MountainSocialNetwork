namespace MountainSocialNetwork.Services.Data.Administrator
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MountainSocialNetwork.Data.Models;

    public interface IAdministratorService
    {
         Task<IEnumerable<T>> GetAllNewsFeedPost<T>();

         Task<IEnumerable<T>> GetAllArticlesPost<T>();

         Task DeleteNewsFeedPost(NewsFeedPost newsFeedPost);

         Task<NewsFeedPost> GetNewsFeedPost(int id);

         Task DeleteArticle(Article article);
 
         Task<Article> GetArticle(int id);

         Task<NewsFeedPost> UpdateNewsFeedPost(NewsFeedPost article);

         Task<Article> UpdateArticle(Article article);

         Task<T> GetByIdNewsFeedPost<T>(int id);

         Task<T> GetByIdArticle<T>(int id);

    }
}
