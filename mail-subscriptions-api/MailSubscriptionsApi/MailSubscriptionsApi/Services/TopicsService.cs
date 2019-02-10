using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailSubscriptionsApi.Data;
using MailSubscriptionsApi.Models;

namespace MailSubscriptionsApi.Services
{
    public class TopicsService : ITopicsService
    {
        private readonly SubscriptionsContext dbContext;

        public TopicsService(SubscriptionsContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Topic> Topics
        {
            get { return this.dbContext.Topics; }
        }
    }
}
