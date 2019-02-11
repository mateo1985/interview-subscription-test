using System;

namespace MailSubscriptionsApi.Exceptions
{
    public class TopicNotExistsException : Exception
    {
        public TopicNotExistsException(string message) : base(message)
        {

        }
    }
}
