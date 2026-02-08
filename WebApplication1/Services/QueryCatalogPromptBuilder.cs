using System.Text;
using WebApplication1.Queries;

namespace WebApplication1.Services
{
    public static class QueryCatalogPromptBuilder
    {
        public static string Build()
        {
            var sb = new StringBuilder();
            sb.AppendLine("You may choose ONLY from the following queries:");
            sb.AppendLine();

            foreach (var q in DashboardQueryDefinitions.All)
            {
                sb.AppendLine($"QueryId: {q.QueryId}");
                sb.AppendLine($"Description: {q.Description}");
                sb.AppendLine($"AllowedParameters: {string.Join(", ", q.AllowedParameters)}");
                sb.AppendLine();
            }

            sb.AppendLine("Return JSON ONLY in this format:");
            sb.AppendLine("{ \"queryId\": \"...\", \"parameters\": { } }");
            sb.AppendLine("If no query applies, return:");
            sb.AppendLine("{ \"queryId\": \"NONE\", \"parameters\": { } }");

            return sb.ToString();
        }
    }
}
