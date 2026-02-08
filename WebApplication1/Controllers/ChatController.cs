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
            var selection = await _aiService.SelectQueryAsync(request.Message);

            if (selection.QueryId == "NONE")
            {
                return Ok(new ChatResponse
                {
                    Reply = "I can answer questions about dashboard data (counts, status, latest record)."
                });
            }

            var answer = await _retrievalService.RetrieveAnswerAsync(
                selection.QueryId,
                selection.Parameters);

            if (answer == null)
            {
                return Ok(new ChatResponse
                {
                    Reply = "That query is not supported."
                });
            }

            return Ok(new ChatResponse
            {
                Reply = answer
            });
        }
    }


}
