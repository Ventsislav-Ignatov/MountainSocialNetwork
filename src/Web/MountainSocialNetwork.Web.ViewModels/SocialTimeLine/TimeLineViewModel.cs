namespace MountainSocialNetwork.Web.ViewModels.SocialTimeLine
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class TimeLineViewModel
    {
        public IEnumerable<TimeLineAllPostsViewModel> AllPosts { get; set; }

        public TimelineCreatePostInputModel TimelineCreatePostInputModel { get; set; }
    }
}
