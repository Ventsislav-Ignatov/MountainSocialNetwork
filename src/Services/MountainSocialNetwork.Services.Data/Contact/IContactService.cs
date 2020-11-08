namespace MountainSocialNetwork.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IContactService
    {
        Task CreateAsync(string name, string email, string title, string content);

    }
}
