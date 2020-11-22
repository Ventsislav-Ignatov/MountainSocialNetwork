namespace MountainSocialNetwork.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IArticleHomePageService
    {
        IEnumerable<T> GetAllArticlePosts<T>(int page, int itemsPerPage = 5);

        Task<IEnumerable<T>> LastThreePosts<T>();

        int GetPostsCount();
    }
}
