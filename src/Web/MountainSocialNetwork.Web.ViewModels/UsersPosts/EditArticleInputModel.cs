namespace MountainSocialNetwork.Web.ViewModels.UsersPosts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class EditArticleInputModel : IMapFrom<Article>
    {
        [Required]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Should be minimum five symbols!")]
        [Display(Name = "Title")]
        [MinLength(5)]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Content")]
        [MinLength(800, ErrorMessage = "Short content! Must be more than 200 symbols!")]
        [MaxLength(30000, ErrorMessage = "Long content! Must be less than 30000 symbols!")]
        public string Content { get; set; }
    }
}
