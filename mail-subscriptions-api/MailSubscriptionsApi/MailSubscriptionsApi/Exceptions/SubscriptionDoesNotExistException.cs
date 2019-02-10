using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailSubscriptionsApi.Exceptions
{
    public class SubscriptionDoesNotExistException: Exception
    {
        public SubscriptionDoesNotExistException(string message): base(message)
        {
        }
    }
}
