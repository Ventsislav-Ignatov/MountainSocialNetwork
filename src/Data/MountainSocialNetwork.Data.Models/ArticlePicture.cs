namespace MountainSocialNetwork.Data.Models
{
    using System;

    using MountainSocialNetwork.Data.Common.Models;

    public class ArticlePicture : BaseModel<int>
    {
        public int ArticleId { get; set; }

        public Article Article { get; set; }

        public string UserId { get; set; }

        public string PictureURL { get; set; }
    }
}
