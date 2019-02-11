using MailSubscriptionsApi.Data;
using MailSubscriptionsApi.Models;
using System.Collections.Generic;

namespace MailSubscriptionsApi.Services
{
    public class TopicsService : ITopicsService
    {
        private readonly SubscriptionsContext dbContext;

        public TopicsService(SubscriptionsContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Gets available topics
        /// </summary>
        public IEnumerable<Topic> Topics
        {
            get { return this.dbContext.Topics; }
        }
    }
}
