namespace MountainSocialNetwork.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MountainSocialNetwork.Data.Common.Models;

    public class Vote : BaseModel<int>
    {
        [Required]
        public int NewsFeedPostId { get; set; }

        public virtual NewsFeedPost NewsFeedPost { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public bool IsUpVote { get; set; }
    }
}
