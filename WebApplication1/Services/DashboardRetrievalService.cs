namespace WebApplication1.Services
{
    using Microsoft.EntityFrameworkCore;
    using WebApplication1.Infrastucture;
    using WebApplication1.Queries;

    public class DashboardRetrievalService
    {
        private readonly AppDbContext _context;

        public DashboardRetrievalService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<string?> RetrieveAnswerAsync(
    string queryId,
    IDictionary<string, string> parameters)
        {
            switch (queryId)
            {
                case DashboardQueryCatalog.TotalRecords:
                    var total = await _context.DashboardRecords.CountAsync();
                    return $"There are {total} total records.";

                case DashboardQueryCatalog.CountByStatus:
                    if (!parameters.TryGetValue("status", out var status))
                        return null;

                    var count = await _context.DashboardRecords
                        .CountAsync(x => x.Status == status);

                    return $"{count} records are {status}.";

                case DashboardQueryCatalog.LatestRecord:
                    var latest = await _context.DashboardRecords
                        .OrderByDescending(x => x.DateCreated)
                        .FirstOrDefaultAsync();

                    return latest == null
                        ? "No records found."
                        : $"The latest record is for property {latest.PropertyName}.";

                default:
                    return null;
            }
        }

    }

}
