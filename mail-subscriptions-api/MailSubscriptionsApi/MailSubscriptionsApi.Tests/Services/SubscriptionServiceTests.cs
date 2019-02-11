using MailSubscriptionsApi.Data;
using MailSubscriptionsApi.Models;
using MailSubscriptionsApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MailSubscriptionsApi.Tests.Services
{
    [TestClass]
    public class SubscriptionServiceTests
    {
        // Tests should be added for the class
        // For other methods and some negative testing should be prepared

        private SubscriptionService cut;

        private DbContextOptions<SubscriptionsContext> options;

        [TestInitialize]
        public void TestInitialize()
        {
            this.options = new DbContextOptionsBuilder<SubscriptionsContext>()
                .UseInMemoryDatabase(databaseName: "SubscriptionsTest").Options;

            using (var context = new SubscriptionsContext(options))
            {
                context.Topics.AddRange(new List<Topic>
                {
                    new Topic{ TopicId = 1, DisplayName = "topic one"},
                    new Topic{ TopicId = 2, DisplayName = "topic two"},
                    new Topic{ TopicId = 3, DisplayName = "topic three"}
                });
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void ShouldSubscribeForTopics()
        {
            //Arrange
            const string user = "test@poczta.fm";
            const int expectedSubscriptions = 2;
            const int expectedUsers = 1;
            Guid resultToken;

            //Act
            using (var context = new SubscriptionsContext(options))
            {
                
                var service = new SubscriptionService(context);
                resultToken = service.SubscribeAsync(user, new [] {1,3}).Result;
            }

            //Assert
            using (var context = new SubscriptionsContext(options))
            {
                Assert.AreEqual(expectedSubscriptions, context.Subscriptions.Count());
                Assert.AreEqual(expectedUsers, context.Users.Count());
                Assert.AreNotEqual(Guid.Empty, resultToken);
            }

            //TODO: Values of the records should be checked as well
        }
    }
}
