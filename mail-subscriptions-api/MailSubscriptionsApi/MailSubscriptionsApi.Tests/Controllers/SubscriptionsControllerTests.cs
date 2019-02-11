using MailSubscriptionsApi.Controllers;
using MailSubscriptionsApi.Models;
using MailSubscriptionsApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;

namespace MailSubscriptionsApi.Tests.Controllers
{
    [TestClass]
    public class SubscriptionsControllerTests
    {
        //Should write much more unit tests, for example for exceptions handling or for unsubscription


        private ISubscriptionService subscriptionService;
        private IMailingService mailingService;
        private ITopicsService topicsService;
        private ILogger<SubscriptionsController> logger;
        private SubscriptionsController cut;

        private const string requestScheme = "http";
        private const string requestHost = "localhost:5200";

        [TestInitialize]
        public void Initialize()
        {
            this.subscriptionService = Substitute.For<ISubscriptionService>();
            this.mailingService = Substitute.For<IMailingService>();
            this.topicsService = Substitute.For<ITopicsService>();
            this.logger = Substitute.For<ILogger<SubscriptionsController>>();

            this.cut = new SubscriptionsController(this.subscriptionService, this.mailingService, this.topicsService, this.logger);
            this.MockControllerContext(requestScheme, requestHost);
        }

        [TestMethod]
        public void ShouldReturnSuccessResult()
        {
            //Arrange
            var userId = "test@user.com";
            var topics = new[] {1, 3};
            var request = new SubscribeRequest {Topics = topics, UserId = userId };
            var expectedToken = Guid.NewGuid();
            this.subscriptionService.SubscribeAsync(Arg.Any<string>(), Arg.Any<int[]>()).Returns(expectedToken);
            var expectedLink =
                $"{requestScheme}://{requestHost}/api/v1/subscriptions?{nameof(UnsubscribeRequest.UnsubscribeGuid)}={expectedToken.ToString()}&{nameof(UnsubscribeRequest.UserId)}={userId}";
            var expectedOkResponse = @"{ unsubscribeLink = " + expectedLink + " }";
            //Act
            var result = this.cut.SubscribeAsync(request).Result;
            
            //Assert
            var value = ((CreatedResult)result).Value.ToString();
            Assert.IsInstanceOfType(result, typeof(CreatedResult));
            Assert.AreEqual(expectedOkResponse, value);
        }

        [TestMethod]
        public void ShouldSendEmailOnSubscription()
        {
            //Arrange
            var userId = "test@user.com";
            var topics = new[] { 1, 3 };
            var request = new SubscribeRequest { Topics = topics, UserId = userId };
            this.topicsService.Topics.Returns(new List<Topic>
            {
                new Topic {TopicId = 1, DisplayName = "Topic one"},
                new Topic {TopicId = 2, DisplayName = "Topic two"},
                new Topic {TopicId = 3, DisplayName = "Topic three"}
            });

            //Act
            this.cut.SubscribeAsync(request).Wait();

            //Assert
            //We do not have to test second argument, it is tested in formatter unit test
            this.mailingService.Received(1).SendEmailAsync(userId, Arg.Any<string>(), "Subscribed");
        }

        private void MockControllerContext(string scheme, string host)
        {
            this.cut.ControllerContext = new ControllerContext();
            this.cut.ControllerContext.HttpContext = new DefaultHttpContext();
            this.cut.ControllerContext.HttpContext.Request.Scheme = scheme;
            this.cut.ControllerContext.HttpContext.Request.Host = new HostString(host);
        }
    }
}
