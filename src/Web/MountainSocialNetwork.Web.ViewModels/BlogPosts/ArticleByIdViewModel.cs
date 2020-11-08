namespace MountainSocialNetwork.Web.ViewModels.BlogPosts
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;
    using MountainSocialNetwork.Web.ViewModels.Comments;
    using MountainSocialNetwork.Web.ViewModels.Pictures;

    public class ArticleByIdViewModel : IMapFrom<Article>
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string UserUserName { get; set; }

        public int CommentsCount { get; set; }

        public int Count { get; set; }

        public IEnumerable<ArticleCommentViewModel> Comments { get; set; }

        public IEnumerable<PicturesViewModel> ArticlePictures { get; set; }
    }
}
