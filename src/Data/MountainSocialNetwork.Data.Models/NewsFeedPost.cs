namespace MountainSocialNetwork.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MountainSocialNetwork.Data.Common.Models;

    public class NewsFeedPost : BaseDeletableModel<int>
    {
        public NewsFeedPost()
        {
            this.NewsFeedComments = new HashSet<NewsFeedComment>();
            this.Votes = new HashSet<Vote>();
        }

        [Required]
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public ICollection<NewsFeedComment> NewsFeedComments { get; set; }

        public ICollection<Vote> Votes { get; set; }
    }
}
