﻿namespace MountainSocialNetwork.Web.ViewModels.SocialTimeLine
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;
    using MountainSocialNetwork.Web.ViewModels.NewsFeed;

    public class TimeLineViewModel
    {
        public IEnumerable<TimeLineAllPostsViewModel> AllPosts { get; set; }


        [Required]
        [MinLength(5, ErrorMessage = "Short content! Must be more than 5 symbols!")]
        [MaxLength(250, ErrorMessage = "Short content! Must be less than 250 symbols!")]
        public string Content { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Town { get; set; }

        public string BirthDay { get; set; }

        public string Description { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string CoverPictureUrl { get; set; }

        public int FriendCount { get; set; }

        public int? RequestFriendCount { get; set; }

        public bool? IsFried { get; set; }

        public int PageNumber { get; set; }

        public int PostsCount { get; set; }

        public int PostsPerPage { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public int PreviousPageNumber => this.PageNumber - 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int NextPageNumber => this.PageNumber + 1;

        public int PagesCount => (int)Math.Ceiling((double)this.PostsCount / this.PostsPerPage);

        public IEnumerable<PostCommentViewModel> NewsComments { get; set; }
    }
}
