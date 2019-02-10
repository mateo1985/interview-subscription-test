using MailSubscriptionsApi.Data;
using MailSubscriptionsApi.Exceptions;
using MailSubscriptionsApi.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MailSubscriptionsApi.Services
{
    public class SubscriptionService: ISubscriptionService
    {
        private readonly SubscriptionsContext dbContext;

        public SubscriptionService(SubscriptionsContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Guid> SubscribeAsync(string userId, int[] topics)
        {
            this.EnsureUserNotExists(userId);
            var unsubscribeToken = Guid.NewGuid();
            var user = new User { UserId = userId, UnsubscribeToken = unsubscribeToken };
            await this.dbContext.Users.AddAsync(user);
            var listOfTopics = topics.Select(x => this.dbContext.Topics.Single(topic => topic.TopicId == x));
            foreach (var topic in listOfTopics)
            {
                await this.dbContext.Subscriptions.AddAsync(new Subscription
                {
                    Topic = topic,
                    User = user
                });
            }

            await this.dbContext.SaveChangesAsync();
            return unsubscribeToken;
        }

        public async Task UnsubscribeAsync(string userId, Guid token)
        {
            var user = this.dbContext.Users.SingleOrDefault(x => x.UserId == userId && x.UnsubscribeToken == token);
            if (user == null)
            {
                throw new SubscriptionDoesNotExistException($"Such subscription does not exist");
            }
            this.dbContext.Users.Remove(user);
            await this.dbContext.SaveChangesAsync();
        }

        private void EnsureUserNotExists(string userId)
        {
            var userExists = this.dbContext.Users.FirstOrDefault(x => x.UserId == userId);
            if (userExists != null)
            {
                throw new UserExistsException($"Subscription for user {userId} already exist");
            }
        }
    }
}
