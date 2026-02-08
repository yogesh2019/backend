using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Infrastucture
{

    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql(
                "Host=dpg-d642o8m3jp1c73bf9fag-a.oregon-postgres.render.com;Port=5432;Database=compldemo;Username=compldemo_user;Password=teZu5RikgjpJdq9mLL20CaM5rHbBE1Qc;SSL Mode=Require;Trust Server Certificate=true"
            );

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
