namespace MountainSocialNetwork.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Migrations;
    using MountainSocialNetwork.Data.Common.Repositories;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class ArticlePostsService : IArticlePostService
    {
        private readonly IDeletableEntityRepository<Article> articleRepository;
        private readonly IRepository<ArticlePicture> articlePictureRepository;
        private readonly IDeletableEntityRepository<UserFavouriteArticle> repository;

        public ArticlePostsService(IDeletableEntityRepository<Article> articleRepository, IRepository<ArticlePicture> articlePictureRepository,
            IDeletableEntityRepository<UserFavouriteArticle> repository)
        {
            this.articleRepository = articleRepository;
            this.articlePictureRepository = articlePictureRepository;
            this.repository = repository;
        }

        public async Task<int> CreateAsync(string title, string content, string userId, int categoryId)
        {
            var post = new Article
            {
                CategoryId = categoryId,
                Content = content,
                Title = title,
                UserId = userId,
            };

            await this.articleRepository.AddAsync(post);
            await this.articleRepository.SaveChangesAsync();

            return post.Id;
        }

        public async Task CreateArticlePicturesAsync(int articleId, string userId, string pictureURL)
        {
            var pictures = new ArticlePicture
            {
                ArticleId = articleId,
                UserId = userId,
                PictureURL = pictureURL,
            };

            await this.articlePictureRepository.AddAsync(pictures);
            await this.articlePictureRepository.SaveChangesAsync();
        }

        public async Task<T> GetById<T>(int id)
        {
            var post = await this.articleRepository.All().Where(a => a.Id == id).To<T>().FirstOrDefaultAsync();
            return post;
        }

        public async Task<IEnumerable<T>> GetAll<T>(string userId)
        {
            return await this.articleRepository.All().Where(a => a.UserId == userId).OrderByDescending(x => x.CreatedOn).To<T>().ToListAsync();
        }

        public async Task<Article> Update(Article article)
        {
            var blogPosts = await this.articleRepository.All().FirstOrDefaultAsync(b => b.Id == article.Id);

            blogPosts.Title = article.Title;
            blogPosts.Content = article.Content;

            await this.articleRepository.SaveChangesAsync();

            return article;
        }

        public async Task<bool> Exists(int id, string authorId)
        {
            return await this.articleRepository.All().AnyAsync(x => x.Id == id && x.UserId == authorId);
        }

        public async Task AddFavouritePost(int articleId, string userId)
        {
            var post = new UserFavouriteArticle
            {
                ArticleId = articleId,
                UserId = userId,
            };

            await this.repository.AddAsync(post);
            await this.repository.SaveChangesAsync();
        }

        public async Task<bool> AlreadyAdded(int articleId, string userId)
        {
            var post = await this.repository.All().Where(a => a.ArticleId == articleId && a.UserId == userId).FirstOrDefaultAsync();

            if (post != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<T>> GetAllFavouritePost<T>(string userId)
        {
            return await this.repository
                    .All()
                    .Where(x => x.UserId == userId)
                    .To<T>()
                    .ToListAsync();
        }
    }
}
