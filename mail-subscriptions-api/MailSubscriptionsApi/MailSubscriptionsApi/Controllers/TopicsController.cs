using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailSubscriptionsApi.Data;
using MailSubscriptionsApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        [HttpGet]
        public async Task<IActionResult> GetTopics()
        {
            return this.Ok(this.topicsService.Topics.Select(x => new { id = x.TopicId, displayName = x.DisplayName }));
        }
    }
}
