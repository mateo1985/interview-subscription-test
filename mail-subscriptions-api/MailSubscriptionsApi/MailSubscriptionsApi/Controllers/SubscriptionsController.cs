using MailSubscriptionsApi.Exceptions;
using MailSubscriptionsApi.Formatters;
using MailSubscriptionsApi.Models;
using MailSubscriptionsApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailSubscriptionsApi.Controllers
{
    /// <summary>
    /// SubscriptionsController is responsible for the subscriptions and unsubscriptions 
    /// </summary>
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

        /// <summary>
        /// Action is invoked when user want to subscribe for some topics
        /// </summary>
        /// <param name="input">Request model for subscription</param>
        /// <returns>Returns unsubscribe link</returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> SubscribeAsync([FromBody] SubscribeRequest input)
        {
            const string unubscriptionPath = "api/v1/subscriptions";
            try
            {
                var unsubscribeToken = await this.subscriptionService.SubscribeAsync(input.UserId, input.Topics);
                var domain = new Uri($"{Request.Scheme}://{Request.Host}");
                var unsubscribeLink =
                    $"{domain}{unubscriptionPath}?{nameof(UnsubscribeRequest.UnsubscribeGuid)}={unsubscribeToken.ToString()}&{nameof(UnsubscribeRequest.UserId)}={input.UserId}";

                //TODO: This formatter below should not be created here - it should be created in some factory the the factory should be injected to this controller
                var formattedMessage = new SubscriptionMessageFormatter(this.GetMatchedTopics(input.Topics), unsubscribeLink);
                await this.mailingService.SendEmailAsync(input.UserId, formattedMessage.Process(), subscribedSubject);
                return Created(unsubscribeToken.ToString(), new
                {
                    unsubscribeLink
                });
            }
            catch (TopicNotExistsException ex)
            {
                this.logger.LogError(ex, "Topic not exists");
                return this.GetBadRequestResult(ex.Message);
            }
            catch (UserExistsException ex)
            {
                this.logger.LogError(ex, "Subscription already exist");
                return this.GetBadRequestResult(ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Subscription error");
                throw;
            }
        }

        /// <summary>
        /// Unsubscribe for messages
        /// </summary>
        /// <param name="input">Unsubscribe model</param>
        /// <returns>Returns information about success or fail</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UnsubscribeAsync([FromQuery] UnsubscribeRequest input)
        {
            try
            {
                await this.subscriptionService.UnsubscribeAsync(input.UserId, input.UnsubscribeGuid);
                await this.mailingService.SendEmailAsync(input.UserId, "You signed out from subscriptions.", unsubscribedSubject);
                return this.Ok("Successfully unsubscribed");
            }
            catch (SubscriptionDoesNotExistException ex)
            {
                this.logger.LogError(ex, "Subscription does not exist");
                return this.GetBadRequestResult(ex.Message);
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

        private IActionResult GetBadRequestResult(string message)
        {
            return this.BadRequest(new
            {
                message
            });
        }
    }
}
