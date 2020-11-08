namespace MountainSocialNetwork.Web.ViewModels.Search
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class SearchInputModel
    {
        [Required]
        [Display(Name = "Type Title...")]
        [MinLength(2)]
        public string Title { get; set; }
    }
}
