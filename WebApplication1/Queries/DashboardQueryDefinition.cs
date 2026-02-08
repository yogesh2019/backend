namespace WebApplication1.Queries
{
    public class DashboardQueryDefinition
    {
        public string QueryId { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public IReadOnlyList<string> AllowedParameters { get; init; } =
            Array.Empty<string>();
    }

}
