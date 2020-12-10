namespace MountainSocialNetwork.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MountainSocialNetwork.Data.Models;

    public interface ICategoriesService
    {
        Task<IEnumerable<T>> GetAll<T>(int? count = null);

        Task<T> CategoriesByName<T>(string name);

        Task<bool> CategoryExits(int id);
    }
}
