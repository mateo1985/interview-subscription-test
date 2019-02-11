using MailSubscriptionsApi.Data;
using MailSubscriptionsApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace MailSubscriptionsApi.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicsService topicsService;

        public TopicsController(SubscriptionsContext dbContext, ITopicsService topicsService)
        {
            this.topicsService = topicsService;
        }

        /// <summary>
        /// Get all topics
        /// </summary>
        /// <returns>List of topics</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetTopicsAsync()
        {
            return this.Ok(this.topicsService.Topics.Select(x => new { id = x.TopicId, displayName = x.DisplayName }));
        }
    }
}
