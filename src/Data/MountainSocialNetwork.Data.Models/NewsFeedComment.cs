namespace MountainSocialNetwork.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MountainSocialNetwork.Data.Common.Models;

    public class NewsFeedComment : BaseDeletableModel<int>
    {
        [Required]
        public int NewsFeedPostId { get; set; }

        public virtual NewsFeedPost NewsFeedPost { get; set; }

        public int? ParentId { get; set; }

        public virtual NewsFeedComment Parent { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
