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
