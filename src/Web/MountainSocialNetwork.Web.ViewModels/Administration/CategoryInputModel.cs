namespace MountainSocialNetwork.Web.ViewModels.Administration
{
    using System.ComponentModel.DataAnnotations;

    public class CategoryInputModel
    {
        [Required]
        public string Name { get; set; }
    }
}
