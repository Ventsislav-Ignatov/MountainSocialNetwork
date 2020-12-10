namespace MountainSocialNetwork.Web.ViewModels.Administration
{
    using System.Collections.Generic;

    public class ArticleCommentResponseModel
    {
        public IEnumerable<ArticleCommentAdministrationViewModel> ArticleComments { get; set; }
    }
}
