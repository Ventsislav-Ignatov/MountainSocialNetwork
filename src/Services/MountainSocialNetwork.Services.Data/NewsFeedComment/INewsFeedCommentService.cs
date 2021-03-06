﻿namespace MountainSocialNetwork.Services.Data.NewsFeed
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MountainSocialNetwork.Web.ViewModels.NewsFeed;

    public interface INewsFeedCommentService
    {
        Task CreateAsync(int newsFeedPostId, string userId, string content, int? parentId = null);

        Task DeleteWhenPostIsDeletedAsync(int postId);
    }
}
