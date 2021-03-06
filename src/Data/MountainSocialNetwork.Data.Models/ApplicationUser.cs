﻿// ReSharper disable VirtualMemberCallInConstructor
namespace MountainSocialNetwork.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;
    using MountainSocialNetwork.Data.Common.Models;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Articles = new HashSet<Article>();
            this.UserFavouriteArticles = new HashSet<UserFavouriteArticle>();
            this.TimeLinePosts = new HashSet<NewsFeedPost>();
            this.UserProfilePictures = new HashSet<UserProfilePicture>();
            this.UserCoverPictures = new HashSet<UserCoverPicture>();
            this.Friends = new HashSet<Friend>();
            this.FriendRequestSend = new HashSet<FriendRequest>();
            this.FriendRequestReceived = new HashSet<FriendRequest>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDay { get; set; }

        [Required]
        public string Town { get; set; }

        public GenderType GenderType { get; set; }

        public string Description { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Article> Articles { get; set; }

        public virtual ICollection<UserFavouriteArticle> UserFavouriteArticles { get; set; }

        public virtual ICollection<NewsFeedPost> TimeLinePosts { get; set; }

        public virtual ICollection<UserProfilePicture> UserProfilePictures { get; set; }

        public virtual ICollection<UserCoverPicture> UserCoverPictures { get; set; }

        public ICollection<Friend> Friends { get; set; }

        public ICollection<FriendRequest> FriendRequestSend { get; set; }

        public ICollection<FriendRequest> FriendRequestReceived { get; set; }
    }
}
