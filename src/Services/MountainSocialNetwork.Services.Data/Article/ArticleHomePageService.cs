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

        public IEnumerable<T> GetAllArticlePostsAsync<T>(int page, int itemsPerPage = 5)
        {
            var posts = this.articleRepository.AllAsNoTracking().OrderByDescending(a => a.CreatedOn)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage).To<T>().ToList();

            return posts;
        }

        public int GetPostsCount()
        {
            return this.articleRepository.AllAsNoTracking().Count();
        }

        public async Task<IEnumerable<T>> LastThreePostsAsync<T>()
        {
            return await this.articleRepository.All().OrderByDescending(a => a.CreatedOn).Take(3).To<T>().ToListAsync();
        }
    }
}
