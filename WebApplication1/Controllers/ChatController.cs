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
        private readonly AiResponseService _aiService;

        public ChatController(
            DashboardRetrievalService retrievalService,
            AiResponseService aiService)
        {
            _retrievalService = retrievalService;
            _aiService = aiService;
        }

        [HttpPost]
        public async Task<IActionResult> Chat([FromBody] ChatRequest request)
        {
            var retrieved = await _retrievalService.RetrieveAnswerAsync(request.Message);

            if (retrieved == null)
            {
                return Ok(new ChatResponse
                {
                    Reply = "I can answer questions about dashboard data (total, accepted, rejected)."
                });
            }

            var aiReply = await _aiService.GenerateAsync(retrieved, request.Message);

            return Ok(new ChatResponse
            {
                Reply = aiReply
            });
        }
    }


}
