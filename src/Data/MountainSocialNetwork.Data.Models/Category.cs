namespace MountainSocialNetwork.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MountainSocialNetwork.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.ArticlePosts = new HashSet<Article>();
        }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Article> ArticlePosts { get; set; }
    }
}
