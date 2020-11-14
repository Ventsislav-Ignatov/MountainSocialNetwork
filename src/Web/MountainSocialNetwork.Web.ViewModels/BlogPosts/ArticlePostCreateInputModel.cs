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
        [RegularExpression("[А-Я]+", ErrorMessage = "Name should start with upper letter.")]
        [Display(Name = "Title")]
        [MinLength(5)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Content")]
        [MinLength(200, ErrorMessage = "Short content! Must be more than 200 symbols!")]
        public string Content { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        public ICollection<IFormFile> Picture { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }

    }
}