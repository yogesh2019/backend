using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApplication1.Entities;

namespace WebApplication1.Infrastucture
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<DashboardRecord> DashboardRecords => Set<DashboardRecord>();
    }
}
