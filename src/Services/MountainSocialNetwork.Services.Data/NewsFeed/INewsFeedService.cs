namespace MountainSocialNetwork.Services.Data.TimeLine
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MountainSocialNetwork.Data.Models;

    public interface INewsFeedService
    {
        Task<int> CreateAsync(string content, string userId);

        IEnumerable<T> GetAllSocialPosts<T>(int? count = null);

        Task<T> GetById<T>(int id);

        Task<bool> ExistsAndOwner(int id, string authorId);

        Task<NewsFeedPost> Update(NewsFeedPost newsFeedPost);

    }
}
