namespace MountainSocialNetwork.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MountainSocialNetwork.Data.Common.Repositories;
    using MountainSocialNetwork.Data.Models;

    public class ContactService : IContactService
    {
        private readonly IRepository<ContactFormEntry> contactsRepository;

        public ContactService(IRepository<ContactFormEntry> contactsRepository)
        {
            this.contactsRepository = contactsRepository;
        }

        public async Task CreateAsync(string name, string email, string title, string content)
        {
            var contactEntry = new ContactFormEntry
            {
                Name = name,
                Email = email,
                Title = title,
                Content = content,
            };

            await this.contactsRepository.AddAsync(contactEntry);
            await this.contactsRepository.SaveChangesAsync();
        }
    }
}
