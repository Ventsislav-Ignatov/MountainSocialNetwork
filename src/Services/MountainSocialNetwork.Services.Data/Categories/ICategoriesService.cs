namespace MountainSocialNetwork.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MountainSocialNetwork.Data.Models;

    public interface ICategoriesService
    {
        Task<IEnumerable<T>> GetAllAsync<T>(int? count = null);

        Task<T> CategoriesByNameAsync<T>(string name);

        Task<bool> CategoryExitsAsync(int id);
    }
}
