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
    using MountainSocialNetwork.Web.ViewModels.SocialTimeLine;

    public class NewsFeedService : INewsFeedService
    {
        private readonly IDeletableEntityRepository<NewsFeedPost> newsFeedRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IRepository<UserProfilePicture> pictureRepository;

        public NewsFeedService(IDeletableEntityRepository<NewsFeedPost> newsFeedRepository, IDeletableEntityRepository<ApplicationUser> userRepository, IRepository<UserProfilePicture> pictureRepository)
        {
            this.newsFeedRepository = newsFeedRepository;
            this.userRepository = userRepository;
            this.pictureRepository = pictureRepository;
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

        public async Task<T> GetById<T>(int id)
        {
            var post = await this.newsFeedRepository.All().Where(a => a.Id == id).To<T>().FirstOrDefaultAsync();
            return post;
        }

        public async Task<bool> ExistsAndOwner(int id, string authorId)
        {
            return await this.newsFeedRepository.All().AnyAsync(x => x.Id == id && x.UserId == authorId);
        }

        //public IEnumerable<T> GetAllSocialPosts<T>(int? count = null)
        //{
        //    IQueryable<NewsFeedPost> timeLinePosts = this.newsFeedRepository.All().OrderByDescending(a => a.CreatedOn);

        //    if (count.HasValue)
        //    {
        //        timeLinePosts = timeLinePosts.Take(count.Value);
        //    }

        //    return timeLinePosts.To<T>().ToList();
        //}

        public async Task<NewsFeedPost> Update(NewsFeedPost newsFeedPost)
        {
            var newPosts = await this.newsFeedRepository.All().FirstOrDefaultAsync(a => a.Id == newsFeedPost.Id);

            newPosts.Content = newsFeedPost.Content;

            await this.newsFeedRepository.SaveChangesAsync();

            return newsFeedPost;
        }

        public async Task Delete(NewsFeedPost newsFeedPost)
        {
            this.newsFeedRepository.Delete(newsFeedPost);

            await this.newsFeedRepository.SaveChangesAsync();
        }

        public async Task<NewsFeedPost> GetNewsFeedPost(int id)
        {
            var post = await this.newsFeedRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            return post;
        }

        public IEnumerable<TimeLineAllPostsViewModel> GetAllSocialPosts(int? count = null)
        {
            var allPost = this.newsFeedRepository.All().OrderByDescending(a => a.CreatedOn)
                .Select(x => new TimeLineAllPostsViewModel
                {
                    Id = x.Id,
                    Content = x.Content,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    CreatedOn = x.CreatedOn,
                    UpVotes = x.Votes.Where(v => v.IsUpVote == true).Count(),
                    DownVotes = x.Votes.Where(d => d.IsUpVote == false).Count(),
                }).ToList();

            return allPost;
        }

        public async Task EditProfile(ApplicationUser user, string userId)
        {
            var allUsers = await this.userRepository.All().ToListAsync();
            var currentUser = await this.userRepository.All().Where(x => x.Id == userId).FirstOrDefaultAsync();

            currentUser.FirstName = user.FirstName;
            currentUser.LastName = user.LastName;
            currentUser.Description = user.Description;
            currentUser.Town = user.Town;
            currentUser.BirthDay = user.BirthDay;

            await this.userRepository.SaveChangesAsync();
        }

        public async Task CreateProfilePicture(string userId, string pictureUrl)
        {
            var userPicture = new UserProfilePicture
            {
                PictureURL = pictureUrl,
                ApplicationUserId = userId,
            };

            await this.pictureRepository.AddAsync(userPicture);
            await this.pictureRepository.SaveChangesAsync();
        }

        public async Task<string> LastPicture(string userId)
        {
            var pictures = await this.pictureRepository.All().Where(x => x.ApplicationUserId == x.ApplicationUserId).OrderByDescending(a => a.CreatedOn).Take(1).FirstOrDefaultAsync();

            return pictures.PictureURL;
        }
    }
}
