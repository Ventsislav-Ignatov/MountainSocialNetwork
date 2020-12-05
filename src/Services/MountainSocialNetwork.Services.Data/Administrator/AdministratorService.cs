namespace MountainSocialNetwork.Services.Data.Administrator
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MountainSocialNetwork.Data.Common.Repositories;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class AdministratorService : IAdministratorService
    {
        private readonly IDeletableEntityRepository<NewsFeedPost> newsFeedRepository;
        private readonly IDeletableEntityRepository<Article> articleRepository;

        public AdministratorService(IDeletableEntityRepository<NewsFeedPost> newsFeedRepository, IDeletableEntityRepository<Article> articleRepository)
        {
            this.newsFeedRepository = newsFeedRepository;
            this.articleRepository = articleRepository;
        }

        public async Task<IEnumerable<T>> GetAllArticlesPost<T>()
        {
            var allPosts = await this.articleRepository.All().OrderByDescending(x => x.CreatedOn).To<T>().ToListAsync();

            return allPosts;
        }

        public async Task<IEnumerable<T>> GetAllNewsFeedPost<T>()
        {
            var allPosts = await this.newsFeedRepository.All().OrderByDescending(x => x.CreatedOn).To<T>().ToListAsync();

            return allPosts;
        }

        public async Task DeleteNewsFeedPost(NewsFeedPost newsFeedPost)
        {
            this.newsFeedRepository.Delete(newsFeedPost);

            await this.newsFeedRepository.SaveChangesAsync();
        }

        public async Task<NewsFeedPost> GetNewsFeedPost(int id)
        {
            var post = await this.newsFeedRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            return post;
        }

        public async Task DeleteArticle(Article article)
        {
            this.articleRepository.Delete(article);

            await this.articleRepository.SaveChangesAsync();
        }

        public async Task<Article> GetArticle(int id)
        {
            var article = await this.articleRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            return article;
        }

        public async Task<NewsFeedPost> UpdateNewsFeedPost(NewsFeedPost post)
        {
            var posts = await this.newsFeedRepository.All().FirstOrDefaultAsync(b => b.Id == post.Id);

            posts.Content = post.Content;

            await this.articleRepository.SaveChangesAsync();

            return posts;
        }

        public async Task<Article> UpdateArticle(Article article)
        {
            var blogPosts = await this.articleRepository.All().FirstOrDefaultAsync(b => b.Id == article.Id);

            blogPosts.Title = article.Title;
            blogPosts.Content = article.Content;

            await this.articleRepository.SaveChangesAsync();

            return blogPosts;
        }

        public async Task<T> GetByIdNewsFeedPost<T>(int id)
        {
            var post = await this.newsFeedRepository.All().Where(a => a.Id == id).To<T>().FirstOrDefaultAsync();
            return post;
        }

        public async Task<T> GetByIdArticle<T>(int id)
        {
            var article = await this.articleRepository.All().Where(a => a.Id == id).To<T>().FirstOrDefaultAsync();
            return article;
        }
    }
}
