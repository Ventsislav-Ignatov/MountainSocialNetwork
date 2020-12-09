namespace MountainSocialNetwork.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MountainSocialNetwork.Data.Models;

    public interface ICommentsService
    {
        Task Create(int articleId, string userId, string content, int? parentId = null);

        bool IsInPostId(int commentId, int articleId);
    }
}
