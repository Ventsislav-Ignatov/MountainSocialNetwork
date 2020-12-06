namespace MountainSocialNetwork.Web.ViewModels.Administration
{
    using System.Collections.Generic;

    public class NewsFeedCommentResponseModel
    {
        public IEnumerable<NewsFeedCommentViewModel> Comments { get; set; }
    }
}
