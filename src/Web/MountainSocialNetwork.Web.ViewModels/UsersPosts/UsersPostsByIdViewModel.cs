namespace MountainSocialNetwork.Web.ViewModels.UsersPosts
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;

    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class UsersPostsByIdViewModel
    {
        public IEnumerable<UserPostByIdModel> Posts { get; set; }

        public IEnumerable<UserFavouriteArticlesViewModel> FavouritePosts { get; set; }

    }
}
