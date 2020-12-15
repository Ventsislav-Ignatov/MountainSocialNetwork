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
    using MountainSocialNetwork.Web.ViewModels.NewsFeed;
    using MountainSocialNetwork.Web.ViewModels.SocialTimeLine;

    public class NewsFeedService : INewsFeedService
    {
        private readonly IDeletableEntityRepository<NewsFeedPost> newsFeedRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IRepository<UserProfilePicture> pictureRepository;
        private readonly IDeletableEntityRepository<NewsFeedComment> commentRepository;
        private readonly IRepository<UserCoverPicture> coverPictureRepository;
        private readonly IDeletableEntityRepository<Friend> friendRepository;

        public NewsFeedService(IDeletableEntityRepository<NewsFeedPost> newsFeedRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository, IRepository<UserProfilePicture> pictureRepository,
            IDeletableEntityRepository<NewsFeedComment> commentRepository, IRepository<UserCoverPicture> coverPictureRepository,
            IDeletableEntityRepository<Friend> friendRepository)
        {
            this.newsFeedRepository = newsFeedRepository;
            this.userRepository = userRepository;
            this.pictureRepository = pictureRepository;
            this.commentRepository = commentRepository;
            this.coverPictureRepository = coverPictureRepository;
            this.friendRepository = friendRepository;
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

        public IEnumerable<T> GetAllSocialPosts<T>(int page, int itemsPerPage = 4)
        {
            var posts = this.newsFeedRepository.All().OrderByDescending(x => x.CreatedOn)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage).To<T>().ToList();

            return posts;
        }

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

        //public IEnumerable<TimeLineAllPostsViewModel> GetAllSocialPosts(int page, int itemsPerPage = 4)
        //{
        //    var allPost = this.newsFeedRepository.All().OrderByDescending(a => a.CreatedOn)
        //      .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
        //      .Select(x => new TimeLineAllPostsViewModel
        //      {
        //          Id = x.Id,
        //          Content = x.Content,
        //          UserUserName = x.User.UserName,
        //          UserFirstName = x.User.FirstName,
        //          UserLastName = x.User.LastName,
        //          CreatedOn = x.CreatedOn,
        //          UpVotes = x.Votes.Where(v => v.IsUpVote == true).Count(),
        //          DownVotes = x.Votes.Where(d => d.IsUpVote == false).Count(),
        //          OwnerPictureUrl = x.User.UserProfilePictures
        //          .Where(a => a.ApplicationUserId == x.UserId)
        //          .OrderByDescending(o => o.CreatedOn)
        //          .Select(s => s.PictureURL)
        //          .FirstOrDefault().ToString(),
        //      }).ToList();

        //    return allPost;
        //}

        public IEnumerable<T> GetAllSocialPostsByUser<T>(string userId, int page, int itemsPerPage = 4)
        {
            var allPost = this.newsFeedRepository.All().Where(x => x.UserId == userId).OrderByDescending(a => a.CreatedOn)
               .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
               .To<T>().ToList();

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

        public async Task<string> LastProfilePicture(string userId)
        {
            var pictures = await this.pictureRepository.All()
                .Where(x => x.ApplicationUserId == userId)
                .OrderByDescending(a => a.CreatedOn).FirstOrDefaultAsync();

            if (pictures != null)
            {
                return pictures.PictureURL;
            }
            else
            {
                return null;
            }
        }

        public async Task<string> LastCoverPicture(string userId)
        {
            var coverPicture = await this.coverPictureRepository.All().Where(x => x.ApplicationUserId == userId).OrderByDescending(a => a.CreatedOn).FirstOrDefaultAsync();

            if (coverPicture != null)
            {
                return coverPicture.PictureURL;
            }
            else
            {
                return null;
            }
        }

        public int GetPostsCount()
        {
            return this.newsFeedRepository.All().Count();
        }

        public int GetPostsCountByUser(string userId)
        {
            return this.newsFeedRepository.All().Where(x => x.UserId == userId).Count();
        }

        public async Task<IEnumerable<PostCommentViewModel>> GetAllComments()
        {
            var comments = await this.commentRepository.AllAsNoTracking()
                .Select(a => new PostCommentViewModel
                {
                    Id = a.Id,
                    Content = a.Content,
                    FirstName = a.User.FirstName,
                    LastName = a.User.LastName,
                    CreatedOn = a.CreatedOn,
                    ParentId = a.ParentId,
                    NewsFeedPostId = a.NewsFeedPostId,
                    PictureURL = a.User.UserProfilePictures
                    .Where(x => x.ApplicationUserId == a.UserId)
                    .OrderByDescending(o => o.CreatedOn)
                    .Select(s => s.PictureURL)
                    .FirstOrDefault().ToString(),
                }).OrderByDescending(x => x.CreatedOn).ToListAsync();

            return comments;
        }

        public async Task CreateCoverPicture(string userId, string pictureUrl)
        {

            var userPicture = new UserCoverPicture
            {
                PictureURL = pictureUrl,
                ApplicationUserId = userId,
            };

            await this.coverPictureRepository.AddAsync(userPicture);
            await this.coverPictureRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllProfilePictures<T>(string userId)
        {
            var profilePictures = await this.pictureRepository.AllAsNoTracking().Where(x => x.ApplicationUserId == userId).To<T>().ToListAsync();

            return profilePictures;
        }

        public async Task<IEnumerable<T>> GetAllCoverPictures<T>(string userId)
        {
            var coverPictures = await this.coverPictureRepository.AllAsNoTracking().Where(x => x.ApplicationUserId == userId).To<T>().ToListAsync();

            return coverPictures;
        }

        public async Task<int> GetFriendCount(string userId)
        {
            var friendCount = await this.friendRepository.All().Where(x => x.ReceiverId == userId || x.SenderId == userId).CountAsync();

            return friendCount;
        }
    }
}
