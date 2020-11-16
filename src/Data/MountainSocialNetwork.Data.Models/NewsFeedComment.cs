namespace MountainSocialNetwork.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MountainSocialNetwork.Data.Common.Models;

    public class NewsFeedComment : BaseDeletableModel<int>
    {
        public int NewsFeedPostId { get; set; }

        public virtual NewsFeedPost NewsFeedPost { get; set; }

        public int? ParentId { get; set; }

        public virtual NewsFeedComment Parent { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
