using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MailSubscriptionsApi.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace MailSubscriptionsApi.Tests.Services
{
    [TestClass]
    public class MailingServiceTests
    {
        private IConfiguration configuration;
        private IRestService restService;

        private MailingService cut;

        private const string mailingServiceUrl = "http://localhost:1023/test";

        [TestInitialize]
        public void Initialize()
        {
            this.configuration = Substitute.For<IConfiguration>();
            this.restService = Substitute.For<IRestService>();

            this.configuration.GetValue<string>(Arg.Any<string>()).Returns(mailingServiceUrl);

            this.cut = new MailingService(this.configuration, this.restService);
        }

        [TestMethod]
        public void ShouldSendEmail()
        {
            //Arrange
            const string destination = "test@poczta.fm";
            const string message = "<h1>test header</h1><div>Sample test message</div>";
            const string subject = "some subject";

            //Act
            this.cut.SendEmailAsync(destination, message, subject).Wait();

            //Assert
            this.restService.Received(1).SendFormEncodedAsync(mailingServiceUrl, Arg.Is<IEnumerable<KeyValuePair<string, string>>>(
                x => 
                    x.ElementAt(0).Value == destination 
                    && x.ElementAt(1).Value == message
                    && x.ElementAt(2).Value == subject));
        }
    }
}
