﻿namespace MountainSocialNetwork.Services.Data.Administrator
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
        private readonly IDeletableEntityRepository<Category> categoryRepository;
        private readonly IDeletableEntityRepository<Comment> commentRepository;

        public AdministratorService(IDeletableEntityRepository<NewsFeedPost> newsFeedRepository, IDeletableEntityRepository<Article> articleRepository,
            IDeletableEntityRepository<NewsFeedComment> newsFeedCommentRepository, IDeletableEntityRepository<Category> categoryRepository,
            IDeletableEntityRepository<Comment> commentRepository)
        {
            this.newsFeedRepository = newsFeedRepository;
            this.articleRepository = articleRepository;
            this.newsFeedCommentRepository = newsFeedCommentRepository;
            this.categoryRepository = categoryRepository;
            this.commentRepository = commentRepository;
        }

        public async Task<IEnumerable<T>> GetAllArticlesPostAsync<T>()
        {
            var allPosts = await this.articleRepository.All().OrderByDescending(x => x.CreatedOn).To<T>().ToListAsync();

            return allPosts;
        }

        public async Task<IEnumerable<T>> GetAllNewsFeedPostAsync<T>()
        {
            var allPosts = await this.newsFeedRepository.All().OrderByDescending(x => x.CreatedOn).To<T>().ToListAsync();

            return allPosts;
        }

        public async Task<IEnumerable<T>> GetAllNewsFeedCommentAsync<T>()
        {
            var comments = await this.newsFeedCommentRepository.All().OrderByDescending(x => x.CreatedOn).To<T>().ToListAsync();

            return comments;
        }

        public async Task DeleteNewsFeedPostAsync(NewsFeedPost newsFeedPost)
        {
            this.newsFeedRepository.Delete(newsFeedPost);

            await this.newsFeedRepository.SaveChangesAsync();
        }

        public async Task<NewsFeedPost> GetNewsFeedPostAsync(int id)
        {
            var post = await this.newsFeedRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            return post;
        }

        public async Task DeleteArticleAsync(Article article)
        {
            var comments = await this.commentRepository.All().Where(x => x.ArticleId == article.Id).ToListAsync();

            foreach (var com in comments)
            {
                await this.DeleteArticleCommentAsync(com.Id);
            }

            this.articleRepository.Delete(article);

            await this.articleRepository.SaveChangesAsync();
        }

        public async Task<Article> GetArticleAsync(int id)
        {
            var article = await this.articleRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            return article;
        }

        public async Task<NewsFeedPost> UpdateNewsFeedPostAsync(NewsFeedPost post)
        {
            var posts = await this.newsFeedRepository.All().FirstOrDefaultAsync(b => b.Id == post.Id);

            posts.Content = post.Content;

            await this.articleRepository.SaveChangesAsync();

            return posts;
        }

        public async Task<Article> UpdateArticleAsync(Article article)
        {
            var blogPosts = await this.articleRepository.All().FirstOrDefaultAsync(b => b.Id == article.Id);

            blogPosts.Title = article.Title;
            blogPosts.Content = article.Content;

            await this.articleRepository.SaveChangesAsync();

            return blogPosts;
        }

        public async Task<T> GetByIdNewsFeedPostAsync<T>(int id)
        {
            var post = await this.newsFeedRepository.All().Where(a => a.Id == id).To<T>().FirstOrDefaultAsync();
            return post;
        }

        public async Task<T> GetByIdArticleAsync<T>(int id)
        {
            var article = await this.articleRepository.All().Where(a => a.Id == id).To<T>().FirstOrDefaultAsync();
            return article;
        }

        public async Task DeleteNewsFeedCommentAsync(NewsFeedComment comment)
        {
            this.newsFeedCommentRepository.Delete(comment);

            await this.newsFeedCommentRepository.SaveChangesAsync();
        }

        public async Task<NewsFeedComment> GetNewsFeedCommentAsync(int id)
        {
            var comment = await this.newsFeedCommentRepository.All().Where(x => x.Id == id).FirstOrDefaultAsync();

            return comment;
        }

        public async Task<NewsFeedComment> UpdateCommentAsync(NewsFeedComment comment)
        {
            var currentComment = await this.newsFeedCommentRepository.All().FirstOrDefaultAsync(b => b.Id == comment.Id);

            currentComment.Content = comment.Content;

            await this.newsFeedCommentRepository.SaveChangesAsync();

            return currentComment;
        }

        public async Task<T> GetByIdCommentAsync<T>(int id)
        {
            var comment = await this.newsFeedCommentRepository.All().Where(a => a.Id == id).To<T>().FirstOrDefaultAsync();
            return comment;
        }

        public async Task<int> CreateCategoryAsync(string name)
        {
            var category = new Category
            {
                Name = name,
                Title = name,
                Description = name,
            };

            await this.categoryRepository.AddAsync(category);
            await this.categoryRepository.SaveChangesAsync();

            return category.Id;
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await this.categoryRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            var articlesInCategory = await this.articleRepository.All().Where(x => x.CategoryId == id).ToListAsync();

            if (category != null)
            {
                foreach (var article in articlesInCategory)
                {
                    this.articleRepository.Delete(article);
                }

                this.categoryRepository.Delete(category);
            }

            await this.categoryRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllCategoriesAsync<T>()
        {

            var allCategory = await this.categoryRepository.All().OrderByDescending(x => x.CreatedOn).To<T>().ToListAsync();

            return allCategory;
        }

        public async Task<IEnumerable<T>> GetAllArticlesCommentAsync<T>()
        {
            var comments = await this.commentRepository.All().OrderBy(x => x.Id).To<T>().ToListAsync();

            return comments;
        }

        public async Task DeleteArticleCommentAsync(int id)
        {
            var comment = await this.commentRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            this.commentRepository.Delete(comment);

            await this.commentRepository.SaveChangesAsync();
        }
    }
}
