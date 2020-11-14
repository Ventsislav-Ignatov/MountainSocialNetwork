namespace MountainSocialNetwork.Services.Data.TimeLine
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

    public class NewsFeedService : INewsFeedService
    {
        private readonly IDeletableEntityRepository<NewsFeedPost> newsFeedRepository;

        public NewsFeedService(IDeletableEntityRepository<NewsFeedPost> newsFeedRepository)
        {
            this.newsFeedRepository = newsFeedRepository;
        }

        public async Task<int> CreateAsync(string content, string userId)
        {
            var timeLinePost = new NewsFeedPost
            {
                Content = content,
                UserId = userId,
            };

            await this.newsFeedRepository.AddAsync(timeLinePost);
            await this.newsFeedRepository.SaveChangesAsync();

            return timeLinePost.Id;
        }

        public async Task<bool> ExistsAndOwner(int id, string authorId)
        {
            return await this.newsFeedRepository.All().AnyAsync(x => x.Id == id && x.UserId == authorId);
        }

        public IEnumerable<T> GetAllSocialPosts<T>(int? count = null)
        {
            IQueryable<NewsFeedPost> timeLinePosts = this.newsFeedRepository.All().OrderByDescending(a => a.CreatedOn);

            if (count.HasValue)
            {
                timeLinePosts = timeLinePosts.Take(count.Value);
            }

            return timeLinePosts.To<T>().ToList();
        }

        public async Task<T> GetById<T>(int id)
        {
            var post = await this.newsFeedRepository.All().Where(a => a.Id == id).To<T>().FirstOrDefaultAsync();
            return post;
        }

        public async Task<NewsFeedPost> Update(NewsFeedPost newsFeedPost)
        {
            var newPosts = await this.newsFeedRepository.All().FirstOrDefaultAsync(a => a.Id == newsFeedPost.Id);

            newPosts.Content = newsFeedPost.Content;

            await this.newsFeedRepository.SaveChangesAsync();

            return newsFeedPost;
        }
    }
}
