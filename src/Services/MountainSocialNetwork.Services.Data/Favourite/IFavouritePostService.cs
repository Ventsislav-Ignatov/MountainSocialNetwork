namespace MountainSocialNetwork.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IFavouritePostService
    {
        Task AddFavouritePost(int articleId, string userId);

        Task<bool> AlreadyAdded(int articleId, string userId);

        Task<IEnumerable<T>> GetAllFavouritePost<T>(string userId);
    }
}
