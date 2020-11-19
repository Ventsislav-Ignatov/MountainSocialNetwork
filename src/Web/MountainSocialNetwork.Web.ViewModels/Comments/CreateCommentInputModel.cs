namespace MountainSocialNetwork.Web.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class CreateCommentInputModel
    {
        [Required]
        public int PostId { get; set; }

        public int ParentId { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(400)]
        public string Content { get; set; }
    }
}
