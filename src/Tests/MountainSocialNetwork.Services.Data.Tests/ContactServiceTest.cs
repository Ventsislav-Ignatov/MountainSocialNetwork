namespace MountainSocialNetwork.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MountainSocialNetwork.Data;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Data.Repositories;
    using Xunit;

    public class ContactServiceTest
    {
        [Fact]
        public async Task CreateShouldWorkCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var dbContext = new ApplicationDbContext(options);

            var contactRepository = new EfRepository<ContactFormEntry>(dbContext);

            var service = new ContactService(contactRepository);

            var newContactRequest = new ContactFormEntry
            {
                Title = "testt",
                Content = "test",
                Email = "test@gmail.com",
                Name = "test",
            };

            var result = service.CreateAsync(newContactRequest.Name, newContactRequest.Email, newContactRequest.Title, newContactRequest.Content);

            Assert.Equal(1, contactRepository.All().Count());

        }
    }
}
