
using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;
using WebApplication1.Infrastucture;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile(
        $"appsettings.{builder.Environment.EnvironmentName}.json",
        optional: true,
        reloadOnChange: true
    )
    .AddEnvironmentVariables();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
        policy
            .WithOrigins(
                "http://localhost:4200",          // local dev
                "https://backend-6xng.onrender.com" // deployed frontend
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});
builder.Services.AddScoped<DashboardRetrievalService>();
builder.Services.AddScoped<AiResponseService>();
builder.Services.AddScoped<GeminiAiResponseService>();

var app = builder.Build(); using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!db.DashboardRecords.Any())
    {
        var records = new List<DashboardRecord>();

        string[] assessors = { "John Smith", "Emily Brown", "Michael Johnson", "Sarah Wilson", "David Clark" };
        string[] reviewers = { "Anna Taylor", "Robert Miller", "Laura Anderson", "James Thomas" };
        string[] propertyNames =
        {
            "Oakwood Apartments",
            "Riverside Residency",
            "Maple Heights",
            "Sunset Villas",
            "Greenfield Homes"
        };

        for (int i = 1; i <= 20; i++)
        {
            records.Add(new DashboardRecord
            {
                Number = 1000 + i,
                DateCreated = DateTime.UtcNow.AddDays(-i),
                Status = i % 4 == 0 ? "Rejected" : "Accepted",
                PropertyReference = $"PROP-{i:000}",
                PropertyName = propertyNames[i % propertyNames.Length],
                PostCode = $"AB{i:00} {i + 10}CD",
                Assessor = assessors[i % assessors.Length],
                Reviewer = reviewers[i % reviewers.Length],
                NextExpectedEvent = DateTime.UtcNow.AddDays(i + 2)
            });
        }

        db.DashboardRecords.AddRange(records);
        db.SaveChanges();
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors("AllowAngular");

app.UseAuthorization();

app.MapControllers();

app.Run();
