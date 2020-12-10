namespace MountainSocialNetwork.Services.Data
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

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;

        }

        // Get all Categories and return to ViewHomePage
        public async Task<IEnumerable<T>> GetAll<T>(int? count = null)
        {
            IQueryable<Category> categories = this.categoryRepository.All().OrderBy(a => a.Name);

            if (count.HasValue)
            {
                categories = categories.Take(count.Value);
            }

            return await categories.To<T>().ToListAsync();
        }

        public async Task<T> CategoriesByName<T>(string name)
        {
            var category = await this.categoryRepository.All().Where(x => x.Name == name).To<T>().FirstOrDefaultAsync();
            return category;
        }

        public async Task<bool> CategoryExits(int id)
        {
            var category = await this.categoryRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            if (category != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
