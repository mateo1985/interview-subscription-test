using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailSubscriptionsApi.Data;
using MailSubscriptionsApi.Exceptions;
using MailSubscriptionsApi.Formatters;
using MailSubscriptionsApi.Models;
using MailSubscriptionsApi.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MailSubscriptionsApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private const string unsubscribedSubject = "Unsubscribed";
        private const string subscribedSubject = "Subscribed";
        private readonly ITopicsService topicsService;
        private readonly ISubscriptionService subscriptionService;
        private readonly IMailingService mailingService;
        private readonly ILogger<SubscriptionsController> logger;

        public SubscriptionsController(ISubscriptionService subscriptionService,
            IMailingService mailingService,
            ITopicsService topicsService,
            ILogger<SubscriptionsController> logger)
        {
            this.topicsService = topicsService;
            this.subscriptionService = subscriptionService;
            this.mailingService = mailingService;
            this.logger = logger;
        }
        
        [HttpPost]
        public async Task<IActionResult> Subscribe([FromBody] SubscribeRequest input)
        {
            try
            {
                var unsubscribeToken = await this.subscriptionService.SubscribeAsync(input.UserId, input.Topics);
                var domain = new Uri($"{Request.Scheme}://{Request.Host}");
                var unsubscribeLink =
                    $"{domain}api/v1/subscriptions?{nameof(UnsubscribeRequest.UnsubscribeGuid)}={unsubscribeToken.ToString()}&{nameof(UnsubscribeRequest.UserId)}={input.UserId}";

                var formattedMessage =
                    new SubscriptionMessageFormatter(this.GetMatchedTopics(input.Topics), unsubscribeLink);
                await this.mailingService.SendEmailAsync(input.UserId, formattedMessage.Process(), subscribedSubject);
                return Created(unsubscribeToken.ToString(), new
                {
                    unsubscribeLink
                });
            }
            catch (UserExistsException ex)
            {
                this.logger.LogError(ex, "Subscription already exist");
                return this.BadRequest(new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Subscription error");
                throw;
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> Unsubscribe([FromQuery] UnsubscribeRequest input)
        {
            try
            {
                await this.subscriptionService.UnsubscribeAsync(input.UserId, input.UnsubscribeGuid);
                await this.mailingService.SendEmailAsync(input.UserId, "You signed out from subscriptions.",
                    unsubscribedSubject);
                return this.Ok("Successfully unsubscribed");
            }
            catch (SubscriptionDoesNotExistException ex)
            {
                this.logger.LogError(ex, "Subscription does not exist");
                return this.BadRequest(new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Subscription error");
                throw;
            }
        }

        private IEnumerable<string> GetMatchedTopics(int[] topicIds)
        {
            return this.topicsService.Topics.Where(x => topicIds.Contains(x.TopicId)).Select(x => x.DisplayName);
        }
    }
}
