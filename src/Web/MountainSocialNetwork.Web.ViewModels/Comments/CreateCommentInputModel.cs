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
        [StringLength(300, ErrorMessage = "Мust be at least two {2} symbols.", MinimumLength = 2)]
        public string Content { get; set; }
    }
}
