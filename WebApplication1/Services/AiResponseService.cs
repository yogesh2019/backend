
using OpenAI.Chat;

namespace WebApplication1.Services
{

    public class AiResponseService
    {
        private readonly ChatClient _chatClient;

        public AiResponseService(IConfiguration configuration)
        {
            var apiKey = configuration["OpenAI:ApiKey"];
            _chatClient = new ChatClient("gpt-4o-mini", apiKey);
        }

        public async Task<string> GenerateAsync(string context, string userQuestion)
        {
            var response = await _chatClient.CompleteChatAsync(
            [
                new SystemChatMessage(
                    "You are an assistant answering questions using ONLY the provided context. " +
                    "Do not invent data."
                ),
                new SystemChatMessage($"Context:\n{context}"),
                new UserChatMessage(userQuestion)
            ]);

            return response.Value.Content[0].Text;
        }
    }
}
