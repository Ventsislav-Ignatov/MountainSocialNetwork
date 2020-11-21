namespace MountainSocialNetwork.Services.Data.TimeLine
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Web.ViewModels.SocialTimeLine;

    public interface INewsFeedService
    {
        Task<int> CreateAsync(string content, string userId);

        IEnumerable<TimeLineAllPostsViewModel> GetAllSocialPosts(int? count = null);

        //IEnumerable<T> GetAllSocialPosts<T>(int? count = null);

        Task<bool> ExistsAndOwner(int id, string authorId);

        Task<NewsFeedPost> Update(NewsFeedPost newsFeedPost);

        Task Delete(NewsFeedPost newsFeedPost);

        Task<NewsFeedPost> GetNewsFeedPost(int id);

        Task<T> GetById<T>(int id);

        Task EditProfile(ApplicationUser user, string userId);

        Task CreateProfilePicture(string userId, string pictureUrl);

        Task<string> LastPicture(string userId);
    }
}
