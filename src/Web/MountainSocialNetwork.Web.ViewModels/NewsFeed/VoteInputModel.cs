namespace MountainSocialNetwork.Web.ViewModels.NewsFeed
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class VoteInputModel
    {
        [Required]
        public int NewsFeedPostId { get; set; }

        [Required]
        public bool IsUpVote { get; set; }
    }
}
