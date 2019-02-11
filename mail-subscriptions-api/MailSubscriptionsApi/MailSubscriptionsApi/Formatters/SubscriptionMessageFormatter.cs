using System.Collections.Generic;

namespace MailSubscriptionsApi.Formatters
{
    public class SubscriptionMessageFormatter: FormatterBase
    {
        private readonly IEnumerable<string> topics;

        private readonly string unsubscribeLink;

        public SubscriptionMessageFormatter(IEnumerable<string> topics, string unsubscribeLink)
        {
            this.topics = topics;
            this.unsubscribeLink = unsubscribeLink;
        }

        public override string Process()
        {
            var topicsHtml = "";
            foreach (var topic in topics)
            {
                topicsHtml += $"<li>{topic}</li>";
            }

            var htmlTemplate = $"<h1>Subscribed for:</h1>"
                               + $"<ul>{topicsHtml}</ul>"
                               + $"<div>To unsubscribe topics press link:</div>"
                               + $"<a href=\"{unsubscribeLink}\">Unsubscribe</a>";

            return htmlTemplate;
        }
    }
}
