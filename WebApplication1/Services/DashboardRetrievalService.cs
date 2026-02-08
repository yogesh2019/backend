namespace WebApplication1.Services
{
    using Microsoft.EntityFrameworkCore;
    using WebApplication1.Infrastucture;

    public class DashboardRetrievalService
    {
        private readonly AppDbContext _context;

        public DashboardRetrievalService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string?> RetrieveAnswerAsync(string userMessage)
        {
            var message = userMessage.ToLower();

            if (message.Contains("total"))
            {
                var total = await _context.DashboardRecords.CountAsync();
                return $"There are {total} total records in the dashboard.";
            }

            if (message.Contains("accepted"))
            {
                var accepted = await _context.DashboardRecords
                    .CountAsync(x => x.Status == "Accepted");

                return $"{accepted} records are accepted.";
            }

            if (message.Contains("rejected"))
            {
                var rejected = await _context.DashboardRecords
                    .CountAsync(x => x.Status == "Rejected");

                return $"{rejected} records are rejected.";
            }

            return null; // not handled
        }
    }

}
