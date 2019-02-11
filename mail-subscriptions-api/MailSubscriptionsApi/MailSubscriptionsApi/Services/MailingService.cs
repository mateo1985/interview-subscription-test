using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailSubscriptionsApi.Services
{
    public class MailingService : IMailingService
    {
        private readonly string mailingServiceUrl;

        private readonly IRestService restService;

        public MailingService(IConfiguration configuration, IRestService restService)
        {
            this.restService = restService;
            this.mailingServiceUrl = configuration.GetValue<string>(ConfigurationConstants.MailingServiceKey);
        }

        public async Task SendEmailAsync(string destination, string htmlMessage, string subject)
        {
            //TODO: below are 'magic strings' which should be move to some static class
            var dataToBeSend = new[]
            {
                new KeyValuePair<string, string>("destination", destination),
                new KeyValuePair<string, string>("message", htmlMessage),
                new KeyValuePair<string, string>("subject", subject)
            };
            await this.restService.SendFormEncodedAsync(this.mailingServiceUrl, dataToBeSend);
        }
    }
}
