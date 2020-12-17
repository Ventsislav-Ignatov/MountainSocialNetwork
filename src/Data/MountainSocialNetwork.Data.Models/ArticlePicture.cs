namespace MountainSocialNetwork.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MountainSocialNetwork.Data.Common.Models;

    public class ArticlePicture : BaseModel<int>
    {
        [Required]
        public int ArticleId { get; set; }

        public Article Article { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string PictureURL { get; set; }
    }
}
