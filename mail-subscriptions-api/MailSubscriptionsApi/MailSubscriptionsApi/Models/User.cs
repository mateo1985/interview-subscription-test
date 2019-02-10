using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace MailSubscriptionsApi.Models
{
    public class User
    {
        public string UserId { get; set; }

        public virtual ICollection<Subscription> Subscriptions { get; set; }

        public Guid UnsubscribeToken { get; set; }
    }
}
