namespace MountainSocialNetwork.Services.Data.NewsFeed
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Web.ViewModels.NewsFeed;
    using MountainSocialNetwork.Web.ViewModels.SocialTimeLine;

    public interface INewsFeedService
    {
        Task<int> CreateAsync(string content, string userId);

        IEnumerable<T> GetAllSocialPosts<T>(int page, int itemsPerPage = 4);

        //IEnumerable<TimeLineAllPostsViewModel> GetAllSocialPosts(int page, int itemsPerPage = 4);

        IEnumerable<T> GetAllSocialPostsByUser<T>(string userId, int page, int itemsPerPage = 4);

        Task<IEnumerable<PostCommentViewModel>> GetAllCommentsAsync();

        Task<bool> ExistsAndOwnerAsync(int id, string authorId);

        Task<NewsFeedPost> UpdateAsync(NewsFeedPost newsFeedPost);

        Task DeleteAsync(NewsFeedPost newsFeedPost);

        Task<NewsFeedPost> GetNewsFeedPostAsync(int id);

        Task<T> GetByIdAsync<T>(int id);

        Task EditProfileAsync(ApplicationUser user, string userId);

        Task CreateProfilePictureAsync(string userId, string pictureUrl);

        Task CreateCoverPictureAsync(string userId, string pictureUrl);

        Task<string> LastProfilePictureAsync(string userId);

        Task<string> LastCoverPictureAsync(string userId);

        int GetPostsCount();

        int GetPostsCountByUser(string userId);

        Task<IEnumerable<T>> GetAllProfilePicturesAsync<T>(string userId);

        Task<IEnumerable<T>> GetAllCoverPicturesAsync<T>(string userId);

        Task<int> GetFriendCountAsync(string userId);
    }
}
