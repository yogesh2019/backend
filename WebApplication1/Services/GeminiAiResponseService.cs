using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace WebApplication1.Services
{
    using System.Text;
    using System.Text.Json;

    public class GeminiAiResponseService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GeminiAiResponseService(IConfiguration config)
        {
            _apiKey = config["Gemini:ApiKey"]!;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://generativelanguage.googleapis.com/v1beta/")
            };
        }

        public async Task<string> GenerateAsync(string context, string question)
        {
            var payload = new
            {
                contents = new[]
                {
                new
                {
                    parts = new[]
                    {
                        new
                        {
                            text =
                                "Answer ONLY using the provided context. Do not invent data.\n\n" +
                                $"Context:\n{context}\n\nQuestion:\n{question}"
                        }
                    }
                }
            }
            };

            var response = await _httpClient.PostAsync(
                $"models/gemini-pro:generateContent?key={_apiKey}",
                new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
            );

            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Gemini error {response.StatusCode}: {body}");
            }

            using var json = JsonDocument.Parse(body);

            return json
                .RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString()!;
        }
    }


}
