namespace MountainSocialNetwork.Services.Data.TimeLine
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ITimeLineService
    {
        Task<int> CreateAsync(string content, string userId);

        IEnumerable<T> GetAllSocialPosts<T>(int? count = null);

    }
}
