namespace MountainSocialNetwork.Services.Data.Search
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ISearchService
    {
        Task<IEnumerable<T>> GetSearchedArticlesAsync<T>(string title);
    }
}
