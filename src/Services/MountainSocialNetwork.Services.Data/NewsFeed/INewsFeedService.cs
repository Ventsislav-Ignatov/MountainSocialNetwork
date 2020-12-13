﻿namespace MountainSocialNetwork.Services.Data.TimeLine
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

        Task<IEnumerable<PostCommentViewModel>> GetAllComments();

        Task<bool> ExistsAndOwner(int id, string authorId);

        Task<NewsFeedPost> Update(NewsFeedPost newsFeedPost);

        Task Delete(NewsFeedPost newsFeedPost);

        Task<NewsFeedPost> GetNewsFeedPost(int id);

        Task<T> GetById<T>(int id);

        Task EditProfile(ApplicationUser user, string userId);

        Task CreateProfilePicture(string userId, string pictureUrl);

        Task CreateCoverPicture(string userId, string pictureUrl);

        Task<string> LastProfilePicture(string userId);

        Task<string> LastCoverPicture(string userId);

        int GetPostsCount();

        int GetPostsCountByUser(string userId);

        Task<IEnumerable<T>> GetAllProfilePictures<T>(string userId);

        Task<IEnumerable<T>> GetAllCoverPictures<T>(string userId);

        Task<int> GetFriendCount(string userId);
    }
}
