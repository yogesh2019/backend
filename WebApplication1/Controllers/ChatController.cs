using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{

    [ApiController]
    [Route("api/chat")]
    public class ChatController : ControllerBase
    {
        private readonly DashboardRetrievalService _retrievalService;

        public ChatController(DashboardRetrievalService retrievalService)
        {
            _retrievalService = retrievalService;
        }

        [HttpPost]
        public async Task<IActionResult> Chat([FromBody] ChatRequest request)
        {
            var retrievedAnswer =
                await _retrievalService.RetrieveAnswerAsync(request.Message);

            if (retrievedAnswer != null)
            {
                return Ok(new ChatResponse
                {
                    Reply = retrievedAnswer
                });
            }

            return Ok(new ChatResponse
            {
                Reply = "I can answer questions about dashboard data (total, accepted, rejected)."
            });
        }
    }
}
