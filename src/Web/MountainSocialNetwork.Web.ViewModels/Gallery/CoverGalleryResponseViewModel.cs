namespace MountainSocialNetwork.Web.ViewModels.Gallery
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CoverGalleryResponseViewModel
    {
        public IEnumerable<CoverGalleryViewModel> UserCoverPictures { get; set; }
    }
}
