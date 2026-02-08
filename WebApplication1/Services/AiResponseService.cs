
using OpenAI.Chat;
using System.Text.Json;
using WebApplication1.Models;

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

        public async Task<QuerySelectionResult> SelectQueryAsync(string userQuestion)
        {
            var systemPrompt =
    "You must return ONLY valid JSON. " +
    "Do not include explanations, markdown, or code fences. " +
    "Do not include text before or after JSON. " +
    "Use only the provided query IDs.";

            var catalogPrompt = QueryCatalogPromptBuilder.Build();

            var response = await _chatClient.CompleteChatAsync(
            [
                new SystemChatMessage(systemPrompt),
                new SystemChatMessage(catalogPrompt),
                new UserChatMessage(userQuestion)
            ]);

            var raw = response.Value.Content[0].Text;

            // TEMP LOG — this will expose the issue
            Console.WriteLine("RAW LLM RESPONSE:");
            Console.WriteLine(raw);

            try
            {
                return JsonSerializer.Deserialize<QuerySelectionResult>(
                    raw,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                ) ?? new QuerySelectionResult { QueryId = "NONE" };
            }
            catch (Exception ex)
            {
                Console.WriteLine("JSON PARSE ERROR: " + ex.Message);
                return new QuerySelectionResult { QueryId = "NONE" };
            }
        }
    }
}
