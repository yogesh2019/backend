using Microsoft.EntityFrameworkCore;
using WebApplication1.Infrastucture;
using WebApplication1.Queries;
namespace WebApplication1.Services
{
   

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

                    var normalizedStatus = status.Trim().ToLower();
                    var count = await _context.DashboardRecords
                        .CountAsync(x => x.Status.ToLower() == normalizedStatus);

                    return $"{count} records are {status}.";

                case DashboardQueryCatalog.LatestRecord:
                    var latest = await _context.DashboardRecords
                        .OrderByDescending(x => x.DateCreated)
                        .FirstOrDefaultAsync();

                    return latest == null
                        ? "No records found."
                        : $"The latest record is for property {latest.PropertyName}.";

                case DashboardQueryCatalog.OldestRecord:
                    var oldest = await _context.DashboardRecords
                        .OrderBy(x => x.DateCreated)
                        .FirstOrDefaultAsync();

                    return oldest == null
                        ? "No records found."
                        : $"The first record was created for {oldest.PropertyName}.";

                case DashboardQueryCatalog.CountByProperty:
                    if (!parameters.TryGetValue("propertyName", out var property))
                        return null;

                    var propertyCount = await _context.DashboardRecords
                        .CountAsync(x => x.PropertyName.ToLower() == property.ToLower());

                    return $"{propertyCount} records exist for property {property}.";

                case DashboardQueryCatalog.RecordsCreatedToday:
                    var today = DateTime.UtcNow.Date;

                    var todayCount = await _context.DashboardRecords
                        .CountAsync(x => x.DateCreated >= today);

                    return $"{todayCount} records were created today.";

                case DashboardQueryCatalog.RecordsCreatedThisMonth:
                    var now = DateTime.UtcNow;

                    var monthCount = await _context.DashboardRecords
                        .CountAsync(x =>
                            x.DateCreated.Year == now.Year &&
                            x.DateCreated.Month == now.Month);

                    return $"{monthCount} records were created this month.";

                case DashboardQueryCatalog.StatusBreakdown:
                    var breakdown = await _context.DashboardRecords
                        .GroupBy(x => x.Status)
                        .Select(g => new { Status = g.Key, Count = g.Count() })
                        .ToListAsync();

                    if (!breakdown.Any())
                        return "No records found.";

                    return string.Join(
                        ", ",
                        breakdown.Select(x => $"{x.Count} {x.Status}")
                    );

                default:
                    return null;
            }
        }


    }

}
