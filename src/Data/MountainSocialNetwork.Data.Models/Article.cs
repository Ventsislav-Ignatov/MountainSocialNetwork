namespace MountainSocialNetwork.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MountainSocialNetwork.Data.Common.Models;

    public class Article : BaseDeletableModel<int>
    {
        public Article()
        {
            this.Comments = new HashSet<Comment>();
            this.UserFavouriteArticles = new HashSet<UserFavouriteArticle>();
            this.ArticlePictures = new HashSet<ArticlePicture>();
        }

        public string Title { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<UserFavouriteArticle> UserFavouriteArticles { get; set; }

        public virtual ICollection<ArticlePicture> ArticlePictures { get; set; }


    }
}
