using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{

    [ApiController]
    [Route("api/chat")]
    public class ChatController : ControllerBase
    {
        [HttpPost]
        public IActionResult Chat([FromBody] ChatRequest request)
        {
            var response = new ChatResponse
            {
                Reply = $"You said: {request.Message}"
            };

            return Ok(response);
        }
    }
}
