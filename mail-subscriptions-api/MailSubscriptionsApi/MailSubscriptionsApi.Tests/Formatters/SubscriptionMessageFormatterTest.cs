using MailSubscriptionsApi.Formatters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailSubscriptionsApi.Tests.Formatters
{
    [TestClass]
    public class SubscriptionMessageFormatterTest
    {
        private SubscriptionMessageFormatter cut;

        [TestMethod]
        public void ShouldPrepareMessageForUser()
        {
            //Arrange
            var expectedMessage = $"<h1>Subscribed for:</h1>"
                                           + $"<ul>"
                                           + $"<li>.NET Framework</li>"
                                           + $"<li>CI/CD</li>"
                                           + $"<li>TeamCity</li>"
                                           + $"</ul>"
                                           + $"<div>To unsubscribe topics press link:</div>"
                                           + $"<a href=\"http://unsubscribe.test/com\">Unsubscribe</a>";
            var listOfTopics = new[]
            {
                ".NET Framework",
                "CI/CD",
                "TeamCity"
            };
            const string unsubscribeLink = "http://unsubscribe.test/com";
            this.cut = new SubscriptionMessageFormatter(listOfTopics, unsubscribeLink);

            //Act
            var resultMessage = this.cut.Process();

            //Assert
            Assert.AreEqual(expectedMessage, resultMessage);
        }
    }
}
