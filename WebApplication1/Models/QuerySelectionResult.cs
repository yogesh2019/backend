namespace WebApplication1.Models
{
    public class QuerySelectionResult
    {
        public string QueryId { get; set; } = string.Empty;
        public Dictionary<string, string> Parameters { get; set; } = new();
    }

}
