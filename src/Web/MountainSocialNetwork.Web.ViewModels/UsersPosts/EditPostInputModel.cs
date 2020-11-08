namespace MountainSocialNetwork.Web.ViewModels.UsersPosts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class EditPostInputModel : IMapFrom<Article>
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        [MinLength(10)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; }
    }
}
