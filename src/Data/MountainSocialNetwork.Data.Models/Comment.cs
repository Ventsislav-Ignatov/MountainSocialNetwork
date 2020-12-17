namespace MountainSocialNetwork.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MountainSocialNetwork.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        [Required]
        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }

        public int? ParentId { get; set; }

        public virtual Comment Parent { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

    }
}
