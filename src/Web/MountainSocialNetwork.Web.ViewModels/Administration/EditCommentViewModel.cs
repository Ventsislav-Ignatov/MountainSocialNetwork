namespace MountainSocialNetwork.Web.ViewModels.Administration
{
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class EditCommentViewModel : IMapFrom<NewsFeedComment>
    {
        public int Id { get; set; }

        public string Content { get; set; }
    }
}
