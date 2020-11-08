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

    public class ArticleByUserService : IArticleByUserService
    {
        private readonly IDeletableEntityRepository<Article> articleRepository;

        public ArticleByUserService(IDeletableEntityRepository<Article> articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        public async Task<Article> Update(Article article)
        {
            var blogPosts = await this.articleRepository.All().FirstOrDefaultAsync(b => b.Id == article.Id);

            blogPosts.Title = article.Title;
            blogPosts.Content = article.Content;

            await this.articleRepository.SaveChangesAsync();

            return article;
        }

        public async Task<IEnumerable<T>> GetAll<T>(string userId)
        {
            return await this.articleRepository.All().Where(a => a.UserId == userId).OrderByDescending(x => x.CreatedOn).To<T>().ToListAsync();
        }

        public async Task<bool> Exists(int id, string authorId)
        {
            return await this.articleRepository.All().AnyAsync(x => x.Id == id && x.UserId == authorId);
        }
    }
}
