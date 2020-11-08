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

    public class ArticlePostsService : IArticlePostsService
    {
        private readonly IDeletableEntityRepository<Article> articleRepository;
        private readonly IRepository<ArticlePicture> articlePictureRepository;

        public ArticlePostsService(IDeletableEntityRepository<Article> articleRepository, IRepository<ArticlePicture> articlePictureRepository)
        {
            this.articleRepository = articleRepository;
            this.articlePictureRepository = articlePictureRepository;
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
    }
}
