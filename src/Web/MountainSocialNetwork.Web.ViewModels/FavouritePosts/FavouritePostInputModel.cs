namespace MountainSocialNetwork.Web.ViewModels.FavouritePosts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class FavouritePostInputModel
    {
        [Required]
        public int PostId { get; set; }
    }
}
