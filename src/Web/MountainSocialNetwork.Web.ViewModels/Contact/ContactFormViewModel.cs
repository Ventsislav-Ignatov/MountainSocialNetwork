namespace MountainSocialNetwork.Web.ViewModels.Contact
{
    using System.ComponentModel.DataAnnotations;

    public class ContactFormViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your name")]
        [Display(Name = "Your Name")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your email")]
        [EmailAddress(ErrorMessage = "Please enter valid email address")]
        [Display(Name = "Your email address")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter title for messages")]
        [StringLength(100, ErrorMessage = "Tile must be at least two {2} and no more than {1} symbols.", MinimumLength = 5)]
        [Display(Name = "Мessages title")]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter title for content")]
        [StringLength(10000, ErrorMessage = "Tile must be at least two {2} symbols.", MinimumLength = 20)]
        [Display(Name = "Мessages content")]
        public string Content { get; set; }
    }
}
