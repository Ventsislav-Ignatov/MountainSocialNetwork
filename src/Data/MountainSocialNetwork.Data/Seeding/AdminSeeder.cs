namespace MountainSocialNetwork.Data.Seeding
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MountainSocialNetwork.Common;
    using MountainSocialNetwork.Data.Models;

    public class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var admin = await userManager.FindByNameAsync(GlobalConstants.AdministratorUsername);

            if (admin == null)
            {
                var user = new ApplicationUser
                {
                    FirstName = GlobalConstants.AdministratorFirstname,
                    LastName = GlobalConstants.AdministratorLastname,
                    BirthDay = DateTime.ParseExact(GlobalConstants.AdministratorBirthday, "dd/MM/yyyy", null),
                    GenderType = GenderType.Male,
                    UserName = GlobalConstants.AdministratorUsername,
                    Email = GlobalConstants.AdministratorUsername,
                    Town = GlobalConstants.AdministratorTown,
                };

                var result = await userManager.CreateAsync(user, GlobalConstants.AdministratorPassword);

                await userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);

            }

        }
    }
}
