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

    public class ArticleHomePageService : IArticleHomePageService
    {
        private readonly IDeletableEntityRepository<Article> articleRepository;

        public ArticleHomePageService(IDeletableEntityRepository<Article> articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        public IEnumerable<T> GetAllArticlePosts<T>(int? count = null)
        {
            IQueryable<Article> articles = this.articleRepository.All().OrderByDescending(a => a.CreatedOn);

            if (count.HasValue)
            {
                articles = articles.Take(count.Value);
            }

            return articles.To<T>().ToList();
        }

        public async Task<IEnumerable<T>> LastThreePosts<T>()
        {
            return await this.articleRepository.All().OrderByDescending(a => a.CreatedOn).Take(3).To<T>().ToListAsync();
        }
    }
}
