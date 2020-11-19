// ReSharper disable VirtualMemberCallInConstructor
namespace MountainSocialNetwork.Data.Models
{
    using System;
    using System.Collections.Generic;

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
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDay { get; set; }

        public string Town { get; set; }

        public bool Gender { get; set; }

        public string Description { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Article> Articles { get; set; }

        public virtual ICollection<UserFavouriteArticle> UserFavouriteArticles { get; set; }

        public virtual ICollection<NewsFeedPost> TimeLinePosts { get; set; }
    }
}
