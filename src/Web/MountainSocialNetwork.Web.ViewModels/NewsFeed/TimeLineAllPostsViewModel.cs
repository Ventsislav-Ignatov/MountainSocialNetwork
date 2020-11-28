namespace MountainSocialNetwork.Web.ViewModels.SocialTimeLine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class TimeLineAllPostsViewModel /*: IMapFrom<NewsFeedPost>*/
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedOn { get; set; }

        public int UpVotes { get; set; }

        public int DownVotes { get; set; }

        public string OwnerPictureUrl { get; set; }

        //public void CreateMappings(IProfileExpression configuration)
        //{
        //    configuration.CreateMap<NewsFeedPost, TimeLineAllPostsViewModel>()
        //        .ForMember(x => x.UpVotes, options =>
        //        {
        //            options.MapFrom(a => a.Votes.Where(a => a.IsUpVote == true).Count());
        //        })
        //        .ForMember(x => x.DownVotes, options =>
        //        {
        //            options.MapFrom(p => p.Votes.Where(v => v.IsUpVote == false).Count());
        //        });
        //}

    }
}
