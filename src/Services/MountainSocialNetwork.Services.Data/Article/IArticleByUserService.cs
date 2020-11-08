namespace MountainSocialNetwork.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MountainSocialNetwork.Data.Models;

    public interface IArticleByUserService
    {
        Task<IEnumerable<T>> GetAll<T>(string userId);

        Task<Article> Update(Article article);

        Task<bool> Exists(int id, string authorId);
    }
}
