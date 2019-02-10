using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MailSubscriptionsApi.Models
{
    public class Topic
    {
        public int TopicId { get; set; }

        public string DisplayName { get; set; }

        public virtual ICollection<Subscription> Subscriptions { get; set; }

    }
}
