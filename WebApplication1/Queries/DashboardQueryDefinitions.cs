namespace WebApplication1.Queries
{
    public static class DashboardQueryDefinitions
    {
        public static readonly IReadOnlyList<DashboardQueryDefinition> All =
            new List<DashboardQueryDefinition>
            {
            new()
            {
                QueryId = DashboardQueryCatalog.TotalRecords,
                Description = "Returns total number of dashboard records",
                AllowedParameters = Array.Empty<string>()
            },
            new()
            {
                QueryId = DashboardQueryCatalog.CountByStatus,
                Description = "Returns count of records filtered by status",
                AllowedParameters = new[] { "status" }
            },
            new()
            {
                QueryId = DashboardQueryCatalog.LatestRecord,
                Description = "Returns the most recently created record",
                AllowedParameters = Array.Empty<string>()
            }
            };
    }

}
