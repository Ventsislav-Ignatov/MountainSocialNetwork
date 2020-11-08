namespace MountainSocialNetwork.Web.ViewModels.Pictures
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class PicturesViewModel : IMapFrom<ArticlePicture>
    {
        public string PictureURL { get; set; }

    }
}
