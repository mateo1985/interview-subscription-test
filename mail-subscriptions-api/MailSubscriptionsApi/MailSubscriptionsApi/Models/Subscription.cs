using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MailSubscriptionsApi.Models
{
    public class Subscription
    {
        public int SubscriptionId { get; set; }

        public User User { get; set; }

        public Topic Topic { get; set; }
    }
}
