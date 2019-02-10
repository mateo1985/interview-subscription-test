using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailSubscriptionsApi.Services
{
    public interface ISubscriptionService
    {
        Task<Guid> SubscribeAsync(string userId, int[] topics);

        Task UnsubscribeAsync(string userId, Guid token);
    }
}
