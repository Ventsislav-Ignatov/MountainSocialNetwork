namespace MountainSocialNetwork.Services.Data.TimeLine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MountainSocialNetwork.Data.Common.Repositories;
    using MountainSocialNetwork.Data.Models;
    using MountainSocialNetwork.Services.Mapping;

    public class TimeLineService : ITimeLineService
    {
        private readonly IDeletableEntityRepository<TimeLinePost> timelineRepository;

        public TimeLineService(IDeletableEntityRepository<TimeLinePost> timelineRepository)
        {
            this.timelineRepository = timelineRepository;
        }

        public async Task<int> CreateAsync(string content, string userId)
        {
            var timeLinePost = new TimeLinePost
            {
                Content = content,
                UserId = userId,
            };

            await this.timelineRepository.AddAsync(timeLinePost);
            await this.timelineRepository.SaveChangesAsync();

            return timeLinePost.Id;
        }

        public IEnumerable<T> GetAllSocialPosts<T>(int? count = null)
        {
            IQueryable<TimeLinePost> timeLinePosts = this.timelineRepository.All().OrderByDescending(a => a.CreatedOn);

            if (count.HasValue)
            {
                timeLinePosts = timeLinePosts.Take(count.Value);
            }

            return timeLinePosts.To<T>().ToList();
        }
    }
}
