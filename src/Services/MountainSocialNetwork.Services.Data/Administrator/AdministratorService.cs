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
        private readonly IDeletableEntityRepository<NewsFeedComment> newsFeedCommentRepository;

        public AdministratorService(IDeletableEntityRepository<NewsFeedPost> newsFeedRepository, IDeletableEntityRepository<Article> articleRepository,
            IDeletableEntityRepository<NewsFeedComment> newsFeedCommentRepository)
        {
            this.newsFeedRepository = newsFeedRepository;
            this.articleRepository = articleRepository;
            this.newsFeedCommentRepository = newsFeedCommentRepository;
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

        public async Task<IEnumerable<T>> GetAllNewsFeedComment<T>()
        {
            var comments = await this.newsFeedCommentRepository.All().OrderByDescending(x => x.CreatedOn).To<T>().ToListAsync();

            return comments;
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

        public async Task DeleteNewsFeedComment(NewsFeedComment comment)
        {
            this.newsFeedCommentRepository.Delete(comment);

            await this.newsFeedCommentRepository.SaveChangesAsync();

        }

        public async Task<NewsFeedComment> GetNewsFeedComment(int id)
        {
            var comment = await this.newsFeedCommentRepository.All().Where(x => x.Id == id).FirstOrDefaultAsync();

            return comment;
        }

        public async Task<NewsFeedComment> UpdateComment(NewsFeedComment comment)
        {
            var currentComment = await this.newsFeedCommentRepository.All().FirstOrDefaultAsync(b => b.Id == comment.Id);

            currentComment.Content = comment.Content;

            await this.newsFeedCommentRepository.SaveChangesAsync();

            return currentComment;
        }

        public async Task<T> GetByIdComment<T>(int id)
        {
            var comment = await this.newsFeedCommentRepository.All().Where(a => a.Id == id).To<T>().FirstOrDefaultAsync();
            return comment;
        }
    }
}
