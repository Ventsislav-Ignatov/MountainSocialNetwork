namespace MountainSocialNetwork.Web.ViewModels.BlogHomePage
{
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class HomeCategoryViewModel : IMapFrom<Category>
    {
        public string Title { get; set; }

        public int ArticlePostsCount { get; set; }
    }
}