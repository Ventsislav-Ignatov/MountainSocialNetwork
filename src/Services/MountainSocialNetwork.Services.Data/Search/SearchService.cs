namespace MountainSocialNetwork.Services.Data.Search
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

    public class SearchService : ISearchService
    {
        private readonly IDeletableEntityRepository<Article> articleRepository;

        public SearchService(IDeletableEntityRepository<Article> articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        public async Task<IEnumerable<T>> GetSearchedArticles<T>(string title)
        {
            var searchArticles = await this.articleRepository.All().Where(a => a.Title.Contains(title)).To<T>().ToListAsync();

            return searchArticles;
        }
    }
}
