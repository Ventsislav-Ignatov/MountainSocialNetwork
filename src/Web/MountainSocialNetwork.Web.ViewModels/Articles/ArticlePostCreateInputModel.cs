namespace MountainSocialNetwork.Web.ViewModels.BlogPosts
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Ganss.XSS;
    using Microsoft.AspNetCore.Http;

    public class ArticlePostCreateInputModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Should be minimum five symbols!")]
        [Display(Name = "Title")]
        [MinLength(5)]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Content")]
        [MinLength(800, ErrorMessage = "Short content! Must be more than 800 symbols!")]
        [MaxLength(30000, ErrorMessage = "Long content! Must be less than 30000 symbols!")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        public ICollection<IFormFile> Picture { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }

    }
}