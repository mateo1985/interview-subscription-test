using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailSubscriptionsApi.Formatters
{
    public class UnsubscribeMessageFormatter: FormatterBase
    {
        public override string Process()
        {
            return $"Unsubscribed successfully";
        }
    }
}
