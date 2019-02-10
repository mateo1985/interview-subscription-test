using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailSubscriptionsApi.Models;

namespace MailSubscriptionsApi.Services
{
    public interface ITopicsService
    {
        IEnumerable<Topic> Topics { get; }
    }
}
